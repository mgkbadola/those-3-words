using System;
using System.Text.RegularExpressions;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using those_3_words.Models;
namespace those_3_words
{
    public partial class Those3Words : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void BtnValidate_click(object sender, EventArgs e)
        {
            var enteredWords = new List<string> { FirstWord.Text.Trim(), SecondWord.Text.Trim(), ThirdWord.Text.Trim() };
            
            if (enteredWords.Where(str => str.Length < 4).Count() > 0)
            {
                lblStatus.Text = "Enter words with length more than 3 characters.";
                pnlStatus.Visible = true;
            }
            else if (enteredWords.Find(str => Regex.Matches(str, "[a-zA-Z]+").Count > 1) != null)
            {
                lblStatus.Text = "Enter only 1 word per field.";
                pnlStatus.Visible = true;
            }
            else
            {
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri("https://api.what3words.com")
                };
                string requestPath = "/v3/autosuggest?input={0}&key={1}";
                requestPath = string.Format(requestPath, string.Join(".", enteredWords), "MDR2WQPY");
                client.DefaultRequestHeaders.Add("Accept", "*/*");
                client.DefaultRequestHeaders.Add("User-Agent", "PostmanRuntime/7.29.0");
                var responseTask = client.GetAsync(requestPath);
                var jsonSuggest = responseTask.Result.Content.ReadAsAsync<SF3WA>().Result;
                List<HashSet<string>> listWords = jsonSuggest.suggestions.Select(obj => obj.words.Split('.').ToHashSet()).ToList();
                int index = -1;
                for (int idx = 0; idx < listWords.Count; idx++) 
                { 
                    if(listWords[idx].SetEquals(new HashSet<string>()))
                    {
                        index = idx;
                        break;
                    }
                }
                if(index == -1)
                {
                    lblStatus.Text = "Given 3 Word Address doesn't exist. Choose from the 3 autosuggested addresses.";
                    autoSuggest1.Text = string.Concat("///", jsonSuggest.suggestions[0].words);
                    autoSuggest2.Text = string.Concat("///", jsonSuggest.suggestions[1].words);
                    autoSuggest3.Text = string.Concat("///", jsonSuggest.suggestions[2].words);
                    autoSuggest1.Visible = true;
                    autoSuggest2.Visible = true;
                    autoSuggest3.Visible = true;
                }
                else
                {
                    enteredWords = jsonSuggest.suggestions[index].words.Split('.').AsEnumerable().ToList();
                    hiddenAddress.Value = string.Concat("///", jsonSuggest.suggestions[index].words);
                    hiddenScore.Value = (int.Parse(hiddenScore.Value) + 1).ToString();
                    lblStatus.Text = string.Format("Selected address: {0}", hiddenAddress.Value);
                }
            }
            BtnContinue.Visible = true;
            pnlStatus.Visible = true;
            BtnValidate.Enabled = true;
        }
        protected void BtnContinue_click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(hiddenAddress.Value))
            {
                lblStatus.Text = "Please select an address";
                return;
            }
            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri("https://api.what3words.com")
            };
            string requestUrl = "/v3/convert-to-coordinates?words={0}&key={1}&language=en";
            requestUrl = string.Format(requestUrl, hiddenAddress.Value.Replace("///", ""), "MDR2WQPY");
            var responseTask = client.GetAsync(requestUrl);
            var jsonCtC = responseTask.Result.Content.ReadAsAsync<CF3WA>().Result;
            hiddenCoords.Value = string.Format("{0},{1}", jsonCtC.coordinates.lat, jsonCtC.coordinates.lng);
            hiddenCountry.Value = jsonCtC.country;
            BtnContinue.Enabled = true;
            pnlStatus.Visible = false;
            pnlOnWater.Visible = true;
        }
        protected void BtnLoW_click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(hiddenType.Value))
            {
                lblType.Text = "Please select one of the two.";
                return;
            }
            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri("https://api.onwater.io")
            };
            string requestUrl = "/api/v1/results/{0}?access_token={1}";
            requestUrl = string.Format(requestUrl, hiddenCoords.Value, "e9y5AHdJ6xNMA8qb6rXX");
            var responseTask = client.GetAsync(requestUrl);
            var jsonOW = responseTask.Result.Content.ReadAsAsync<OnWaterResponse>().Result;
            if (hiddenType.Value.Equals("Water") == jsonOW.water)
                hiddenScore.Value = (int.Parse(hiddenScore.Value) + 1).ToString();
            else
                hiddenType.Value = hiddenType.Value.Equals("Water") ? land.Text : water.Text;
            pnlGame.Visible = true;
            lblLoW.Text += string.Format(" {0}", hiddenType.Value);
            if (hiddenCountry.Value.Equals("ZZ"))
                lblContinent.Text = "It is an ocean.";
            else
                lblContinent.Text += string.Format(" {0}", GetContinentFromCode(hiddenCountry.Value));
            pnlOnWater.Visible = false;
        }
        protected void BtnGuess_click(object sender, EventArgs e)
        {
            string txtGuess = GuessMade.Text.Trim();
            if (txtGuess.Length == 0)
            {
                lblChance.Text = "Enter something. " + lblChance.Text;
                BtnGuess.Enabled = true;
            }
            else
            {
                foreach (var alias in hiddenCountry.Value.Split('|'))
                {
                    if (alias.ToLower().Equals(txtGuess.ToLower()))
                    {
                        hiddenScore.Value = (int.Parse(hiddenScore.Value) + int.Parse(hiddenTry.Value)).ToString();
                        lblChance.Text = string.Format("Congrats! You have won the game. Total points: {0}", hiddenScore.Value);
                        BtnGuess.Enabled = false;
                        return;
                    }
                }
                hiddenTry.Value = (int.Parse(hiddenTry.Value) - 1).ToString();
                if (!hiddenTry.Value.Equals("0"))
                {
                    lblChance.Text = string.Format("You have {0} chances left.", hiddenTry.Value);
                    //todo add hint
                    BtnGuess.Enabled = true;
                }
                else
                {
                    lblChance.Text = string.Format("You have lost! Correct answer:<br>", string.Join(" OR ", hiddenCountry.Value.Split('|')));
                    BtnGuess.Enabled = false;
                }
            }
            
        }
        protected string GetContinentFromCode(string alphaCode)
        {
            using(var sr = new StreamReader(string.Format("{0}\\App_Data\\ccr.json", AppContext.BaseDirectory))) 
            {
                var jsonData = JsonSerializer.Deserialize<Dictionary<string, CCR>>(sr.ReadToEnd());
                hiddenCountry.Value = jsonData[alphaCode].Region;
                return jsonData[alphaCode].Continent;
            }
        }
        protected void autoSuggest1_Toggle(object sender, EventArgs e)
        {
            if (autoSuggest1.Checked)
            {
                hiddenAddress.Value = autoSuggest1.Text;
                autoSuggest2.Checked = false;
                autoSuggest3.Checked = false;
            }
            else
                hiddenAddress.Value = string.Empty;
        }
        protected void autoSuggest2_Toggle(object sender, EventArgs e)
        {
            if (autoSuggest2.Checked)
            {
                hiddenAddress.Value = autoSuggest2.Text;
                autoSuggest1.Checked = false;
                autoSuggest3.Checked = false;
            }
                
            else
                hiddenAddress.Value = string.Empty;
        }
        protected void autoSuggest3_Toggle(object sender, EventArgs e)
        {
            if (autoSuggest3.Checked)
            {
                hiddenAddress.Value = autoSuggest3.Text;
                autoSuggest1.Checked = false;
                autoSuggest2.Checked = false;
            }
            else
                hiddenAddress.Value = string.Empty;
        }
        protected void land_Toggle(object sender, EventArgs e)
        {
            if (land.Checked)
            {
                hiddenType.Value = land.Text;
                water.Checked = false;
            }
            else
                hiddenType.Value = string.Empty;
        }
        protected void water_Toggle(object sender, EventArgs e)
        {
            if (water.Checked)
            {
                hiddenType.Value = water.Text;
                land.Checked = false;
            }
            else
                hiddenType.Value = string.Empty;
        }
        protected void BtnReset_click(object sender, EventArgs e)
        {
            FirstWord.Text = string.Empty;
            SecondWord.Text = string.Empty;
            ThirdWord.Text = string.Empty;
            
            lblStatus.Text = string.Empty;
            lblContinent.Text = "It lies in the continent: ";
            lblLoW.Text = "Given coordinates are on: ";
            lblType.Text = "Is it on land or water?";
            lblChance.Text = "You have 3 chances left.";

            water.Checked = false;
            land.Checked = false;
            autoSuggest1.Checked = false;
            autoSuggest2.Checked = false;
            autoSuggest3.Checked = false;

            hiddenAddress.Value = string.Empty;
            hiddenCoords.Value = string.Empty;
            hiddenCountry.Value = string.Empty;
            hiddenScore.Value = "0";
            hiddenTry.Value = "3";

            pnlStatus.Visible = false;
            pnlOnWater.Visible = false;
            pnlGame.Visible = false;

            autoSuggest1.Visible = false;
            autoSuggest2.Visible = false;
            autoSuggest3.Visible = false;

            BtnContinue.Enabled = true;
            BtnGuess.Enabled = true;
            BtnLoW.Enabled = true;
            BtnValidate.Enabled = true;
        }
        protected class CCR
        {
            public string Continent { get; set; }
            public string Region { get; set; }
        }
    }
}