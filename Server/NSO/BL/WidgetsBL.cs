using System;
using NSO.Models;
using Newtonsoft.Json;
using NSO.Requests;
using System.Text.RegularExpressions;

namespace NSO.BL
{
	static public class WidgetsBL
	{
        private static readonly string path = @"widgetsJson.json";
        private static string prefPattern = @"\[\{";
        private static string prefReplacement = "{\"widgets\":[{";
        private static string sufPattern = @"\}\]";
        private static string sufReplacement = "}]}";

        public static List<Widget> GetWidgetsList()
        {
            try
            {
                var response = getListFromJson();
                response = convertMagicNumber(response);
                return response;
            }
            catch(Exception ex)
            {
                Console.WriteLine("{0} Exception caught.", ex);
                throw;
            }
        }

        public static async Task<List<Widget>> AddWidgetBL(AddWidgetReq req)
        {
            try
            {
                var jsonListBeforeAdd = getListFromJson();
                var lastItem = jsonListBeforeAdd.LastOrDefault();
                Widget newWidget = new Widget
                {
                    id =  lastItem != null ? lastItem.id + 1 : 1,
                    Name = req.Name,
                    MagicNumber = req.MagicNumber
                };
                jsonListBeforeAdd.Add(newWidget);
                var newJson = JsonConvert.SerializeObject(jsonListBeforeAdd);
                newJson = Regex.Replace(newJson, prefPattern, prefReplacement);
                newJson = Regex.Replace(newJson, sufPattern, sufReplacement);
                await File.WriteAllTextAsync(path,newJson);
                var listAfterAdd = getListFromJson();
                listAfterAdd = convertMagicNumber(listAfterAdd);

                return listAfterAdd;
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0} Exception caught.", ex);
                throw;
            }
        }
        
        public static async Task<List<Widget>> UpdateWidgetBL(AddWidgetReq req)
        {
            try
            {
                var jsonListBeforeAdd = getListFromJson();
                int index = jsonListBeforeAdd.FindIndex(w => w.id == req.Id);
                jsonListBeforeAdd[index] = new Widget
                {
                    id = req.Id,
                    Name = req.Name,
                    MagicNumber = req.MagicNumber
                };
                var newJson = JsonConvert.SerializeObject(jsonListBeforeAdd);
                newJson = Regex.Replace(newJson, prefPattern, prefReplacement);
                newJson = Regex.Replace(newJson, sufPattern, sufReplacement);
                await File.WriteAllTextAsync(path, newJson);
                var listAfterAdd = getListFromJson();
                listAfterAdd = convertMagicNumber(listAfterAdd);

                return listAfterAdd;
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0} Exception caught.", ex);
                throw;
            }
        }
        
        public static async Task<List<Widget>> RemoveWidgetBL(AddWidgetReq req)
        {
            try
            {
                var jsonListBeforeAdd = getListFromJson();
                jsonListBeforeAdd.RemoveAll(w => w.id == req.Id);
                var newJson = JsonConvert.SerializeObject(jsonListBeforeAdd);
                newJson = Regex.Replace(newJson, prefPattern, prefReplacement);
                newJson = Regex.Replace(newJson, sufPattern, sufReplacement);
                await File.WriteAllTextAsync(path, newJson);
                var listAfterAdd = getListFromJson();
                listAfterAdd = convertMagicNumber(listAfterAdd);

                return listAfterAdd;
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0} Exception caught.", ex);
                throw;
            }
        }

        #region Private Methods
        private static List<Widget> getListFromJson()
        {
            WidgetsList widgetsList = new WidgetsList();
            var widgetsFromJson = JsonConvert.DeserializeObject<WidgetsList>(File.ReadAllText(path));
            widgetsList.Widgets = widgetsFromJson != null && widgetsFromJson.Widgets != null && widgetsFromJson.Widgets.Count > 0 ? widgetsFromJson.Widgets.OrderBy(w => w.id).ToList() : new List<Widget>();
            return widgetsList.Widgets;
        }

        private static string numberToWords(int number)
        {
            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus " + numberToWords(Math.Abs(number));

            string words = "";

            if ((number / 1000000000) > 0)
            {
                words += numberToWords(number / 1000000000) + " billion ";
                number %= 1000000000;
            }

            if ((number / 1000000) > 0)
            {
                words += numberToWords(number / 1000000) + " million ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += numberToWords(number / 1000) + " thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += numberToWords(number / 100) + " hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "and ";

                var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
                var tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }

            return words;
        }

        private static List<Widget> convertMagicNumber(List<Widget> list)
        {
            foreach(var item in list)
            {
                item.MagicNumberStr = numberToWords(item.MagicNumber);
            }
            return list;
        }
        #endregion
    }
}

