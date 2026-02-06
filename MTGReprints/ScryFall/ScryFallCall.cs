using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http;
using System.Diagnostics;

namespace MTGReprints.ScryFall
{

    public class ScryFallCall
    {
        public async Task<List<Card>> GetCardsFromASetTest(string SetName)
        {
            List<Card> reprintList = null;

            Debug.WriteLine("hello world");

            // 1. Instantiate HttpClient
            HttpClient httpClient = new HttpClient();

            // 2. Set the mandatory User-Agent header (Required by Scryfall)
            httpClient.DefaultRequestHeaders.Add("User-Agent", "ScryfallConsoleTest/1.0 (My C# Console App)");

            // 3. Define the card endpoint and card name

            string apiUrl = $"https://api.scryfall.com/cards/search?include_extras=true&include_variations=true&order=set&q=e%3Aaer&unique=prints";

            if (SetName != "")
            {
                apiUrl = SetName;
            }

            Debug.WriteLine($"Calling WebAPI for:...");

            // 4. Use await to asynchronously wait for the GET request to finish
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("API Request Successful!");

                    // 5. Use await to asynchronously read the content (raw JSON string)
                    string jsonContent = await response.Content.ReadAsStringAsync();

                    // The output will be the raw JSON for the card
                    Debug.WriteLine("--- Card Data (JSON) ---");
                    Debug.WriteLine(jsonContent);
                    Debug.WriteLine("--------------------------");


                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    //Card card = JsonSerializer.Deserialize<Card>(jsonContent, options);
                    Set set = JsonSerializer.Deserialize<Set>(jsonContent, options);


                    //string firstCardString = set.Data[0].ToString();

                    //Card firstCard = JsonSerializer.Deserialize<Card>(firstCardString, options);


                    //foreach (var item in set.Data)
                    //{
                    //    Card card = JsonSerializer.Deserialize<Card>(item.ToString(), options);
                    //    if (card.Reprint == true)
                    //    {
                    //        reprintList.Add(card);
                    //    }
                    //}


                    reprintList = set.Data.Select(p => JsonSerializer.Deserialize<Card>(p.ToString(), options)).Where(card => card.Reprint == true).ToList();

                    bool hasMore = set.HasMore;

                    while (hasMore == true)
                    {
                        string newUrl = set.NextPage;
                        HttpResponseMessage response2 = await httpClient.GetAsync(newUrl);

                        if (response2.IsSuccessStatusCode)
                        {

                            string jsonContent2 = await response2.Content.ReadAsStringAsync();

                            Set set2 = JsonSerializer.Deserialize<Set>(jsonContent2, options);

                            reprintList.AddRange(set2.Data.Select(p => JsonSerializer.Deserialize<Card>(p.ToString(), options)).Where(card => card.Reprint == true).ToList());

                            hasMore = set2.HasMore;
                        }

                    }

                    reprintList.ForEach(p => Debug.WriteLine(p.Name));

                    reprintList.ForEach(p => {
                        Debug.WriteLine(p.Name);
                    });

                }
                else
                {
                    Debug.WriteLine($"Error: API call failed with status code {response.StatusCode}");
                    // Optionally read the error content
                    string errorContent = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine($"Error Body: {errorContent}");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An exception occurred: {ex.Message}");
            }

            Debug.WriteLine("THE END");

            return reprintList;

        }


        public async Task<List<SetDef>> GetSetIconsAsync()
        {
            List<SetDef> setDefs = new List<SetDef>();

            Debug.WriteLine("hello world");

            // 1. Instantiate HttpClient
            HttpClient httpClient = new HttpClient();

            // 2. Set the mandatory User-Agent header (Required by Scryfall)
            httpClient.DefaultRequestHeaders.Add("User-Agent", "ScryfallConsoleTest/1.0 (My C# Console App)");

            // 3. Define the card endpoint and card name
            string apiUrl = $"https://api.scryfall.com/sets";

            Debug.WriteLine($"Calling WebAPI for all SETS");

            // 4. Use await to asynchronously wait for the GET request to finish
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("API Request Successful!");

                    // 5. Use await to asynchronously read the content (raw JSON string)
                    string jsonContent = await response.Content.ReadAsStringAsync();

                    // The output will be the raw JSON for the card
                    Debug.WriteLine("--- ALL SETS (JSON) ---");
                    Debug.WriteLine(jsonContent);
                    Debug.WriteLine("--------------------------");


                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    AllSets allSets = JsonSerializer.Deserialize<AllSets>(jsonContent, options);

                    DateTime targetDate = new DateTime(2024, 1, 1);


                    foreach (var item in allSets.Data)
                    {
                        SetDef setD = JsonSerializer.Deserialize<SetDef>(item.ToString(), options);

                        DateTime.TryParse(setD.ReleasedAt, out DateTime parsedDate);

                        if (parsedDate > targetDate && parsedDate < DateTime.Now && setD.SetType == "expansion")
                        {
                            setDefs.Add(setD);
                        }
                    }

                }
                else
                {
                    Debug.WriteLine($"Error: API call failed with status code {response.StatusCode}");
                    // Optionally read the error content
                    string errorContent = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine($"Error Body: {errorContent}");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An exception occurred: {ex.Message}");
            }

            Debug.WriteLine("THE END");

            return setDefs;
        }

    }


}
