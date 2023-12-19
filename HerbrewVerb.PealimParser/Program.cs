using HerbrewVerb.PealimParser;
using HtmlAgilityPack;
using System.Text;

const string VerbUrl = "https://www.pealim.com/ru/dict/1296-lesovev/";

var html = new HtmlWeb();

var document = html.Load(VerbUrl);

if (document == null)
{
    Console.WriteLine("Data not retrieved");
    return;
}

var Binyan = document.GetBinyan();

var strBuild = new StringBuilder();
strBuild.AppendLine("Binyan: " + Binyan);

var Shoresh = document.GetShoresh();
strBuild.AppendLine("Shoresh: " + Shoresh);

var Infinitive = document.GetVerbForm(VerbForm.Infinitive);
strBuild.AppendLine("Infinitive: " + Infinitive);

var InfinitiveTranscript = document.GetVerbFormTranscript(VerbForm.Infinitive);
strBuild.AppendLine("\t\t" + InfinitiveTranscript);



foreach (var form in VerbForm.Active)
{
    var VerbF = document.GetVerbForm(form);
    var VerbFormTranscript = document.GetVerbFormTranscript(form);
    strBuild.AppendLine(form + ": " + VerbF + "");
    strBuild.AppendLine("\t\t" + VerbFormTranscript);
}

if (Binyan == "hИФЪИЛЬ" || Binyan == "ПИЭЛЬ")
{
    foreach (var form in VerbForm.Passive)
    {
        var VerbF = document.GetVerbForm(form);
        var VerbFormTranscript = document.GetVerbFormTranscript(form);
        strBuild.AppendLine(form + ": " + VerbF + "");
        strBuild.AppendLine("\t\t" + VerbFormTranscript);
    }
}

File.WriteAllText("out.txt", strBuild.ToString());






