using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Web;

namespace stockshopkycbot
{
    public enum Sex
    {
        Male,Femal
    }
    public enum Race
    {
        black,white,indian,asian
    }
    [Serializable]
    public class KYC
    {
        [Prompt("please tel us your name")]
        public string Name { get; set; }
        public string Surname { get; set; }
        public string IdNumber { get; set; }
        public string streetAddress { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public string Code { get; set; }
        public Sex Sex { get; set; }
        public Race Race { get; set; }
        [Numeric(1,10)]
        [Optional]
        [Describe("Your experience today")]
        public double? Rating { get; set; }

        public  static IForm<KYC> BuildForm()
        {
            OnCompletionAsyncDelegate<KYC> processInfo = async (context, state) =>
            {
                await context.PostAsync("we are busy verifing your details");
            };

            return new FormBuilder<KYC>()
                .Message("Welcome to Stock shop KYC bot !")
                .AddRemainingFields()
                .OnCompletion(processInfo)
                .Build();
        }

        //public static IForm<JObject> BuildJsonForm()
        //{
        //    using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("stockshopkycbot.stockshopkycbot.KYC.json"))
        //    {
        //        var schema = JObject.Parse(new StreamReader(stream).ReadToEnd());
        //        return new FormBui

        //    }
        //}
        // Cache of culture specific forms. 
        private static ConcurrentDictionary<CultureInfo, IForm<KYC>> _forms = new ConcurrentDictionary<CultureInfo, IForm<KYC>>();

        public static IForm<KYC> BuildLocalizedForm()
        {
            var culture = Thread.CurrentThread.CurrentCulture;
            IForm<KYC> form;
            if (!_forms.TryGetValue(culture, out form))
            {
                OnCompletionAsyncDelegate<KYC> processInfo = async (context, state) =>
                {
                    await context.PostAsync("Please wait while we check your fica status");
                };

                var builder = new FormBuilder<KYC>()
                    .Message("Welcome to Stock shop KYC bot!")
                    .AddRemainingFields()
                    .OnCompletion(processInfo);
                form = builder.Build();
                _forms[culture] = form;

            }
            return form;
        }

    }
}