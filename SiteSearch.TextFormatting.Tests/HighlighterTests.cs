using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using SiteSearch.TextFormatting;

namespace SiteSearch.TextFormatting.Tests
{
  [TestClass]
  public class HighlighterTests
  {

    [TestMethod]
    public void Highlighter1()
    {

      var connectionString = @"data source=MY-PC\SQLEXPRESS;";

      var pattern = @"(data source=)((\w|\-)+?\\\w+?)\;";
      var newConnectionString1 = Regex.Replace(connectionString, pattern, "$1" + "something");

      connectionString = @"data source=xMY-PC\SQLEXPRESS;";
      pattern = @"(data source=)(x)((\w|\-)+?\\\w+?)\;";
      var newConnectionString2 = Regex.Replace(connectionString, pattern, "$1" + "something" + "$2");

      connectionString = @"data source=x,MY-PC\SQLEXPRESS;";
      pattern = @"(data source=)(x,)((\w|\-)+?\\\w+?)\;";
      var newConnectionString3 = Regex.Replace(connectionString, pattern, "$1" + "something" + "$2");

      connectionString = @"data source=,MY-PC\SQLEXPRESS;";
      pattern = @"(data source=)(,)((\w|\-)+?\\\w+?)\;";
      var newConnectionString4 = Regex.Replace(connectionString, pattern, "$1something$2");


      string result = HighlightMatch.HighlightWord("the brown cow is not a dog", "cow", "strong");
      Assert.AreEqual(result, "the brown <strong>cow</strong> is not a dog");

      result = HighlightMatch.HighlightWord("the browncow is not a dog", "cow", "strong");
      Assert.AreEqual(result, "the browncow is not a dog");

      result = HighlightMatch.HighlightWord("the browncow is not a dog", "the", "strong");
      Assert.AreEqual(result, "<strong>the</strong> browncow is not a dog");

      result = HighlightMatch.HighlightWord("the browncow is not a dog", "dog", "strong");
      Assert.AreEqual(result, "the browncow is not a <strong>dog</strong>");

      result = HighlightMatch.HighlightWord("the browncow is the not a dog the", "the", "strong");
      Assert.AreEqual(result, "<strong>the</strong> browncow is <strong>the</strong> not a dog <strong>the</strong>");

      result = HighlightMatch.HighlightWord("the the1 the", "the1", "strong");
      Assert.AreEqual(result, "the <strong>the1</strong> the");

      result = HighlightMatch.HighlightWord("the the the the the", "the", "strong");
      Assert.AreEqual(result, "<strong>the</strong> <strong>the</strong> <strong>the</strong> <strong>the</strong> <strong>the</strong>");

      result = HighlightMatch.HighlightWord("the brown cow, is not a dog", "cow", "strong");
      Assert.AreEqual(result, "the brown <strong>cow,</strong> is not a dog");

      result = HighlightMatch.HighlightWord("the brown cow,, is not a dog", "cow", "strong");
      Assert.AreEqual(result, "the brown <strong>cow,,</strong> is not a dog");

      result = HighlightMatch.HighlightWord("the brown cow,. is not a dog", "cow", "strong");
      Assert.AreEqual(result, "the brown <strong>cow,.</strong> is not a dog");

      result = HighlightMatch.HighlightWord("the brown cow,.\" is not a dog", "cow", "strong");
      Assert.AreEqual(result, "the brown <strong>cow,.\"</strong> is not a dog");

      result = HighlightMatch.HighlightWord("the brown cow,.\", is not a dog", "cow", "strong");
      Assert.AreEqual(result, "the brown <strong>cow,.\",</strong> is not a dog");

      result = HighlightMatch.HighlightWord("the brown cow'- is not a dog", "cow", "strong");
      Assert.AreEqual(result, "the brown <strong>cow'-</strong> is not a dog");

      result = HighlightMatch.HighlightWord("the brown cow,( is not a dog", "cow", "strong");
      Assert.AreEqual(result, "the brown <strong>cow,(</strong> is not a dog");

      result = HighlightMatch.HighlightWord("the brown cow.,(), is not a dog", "cow", "strong");
      Assert.AreEqual(result, "the brown <strong>cow.,(),</strong> is not a dog");

      result = HighlightMatch.HighlightWord("the brown (cow is not a dog", "(cow", "strong");
      Assert.AreEqual(result, "the brown <strong>(cow</strong> is not a dog");

      result = HighlightMatch.HighlightWord("the brown (cow) is not a dog", "(cow)", "strong");
      Assert.AreEqual(result, "the brown <strong>(cow)</strong> is not a dog");

      result = HighlightMatch.HighlightWord("the brown (cow)! is not a dog", "(cow)!", "strong");
      Assert.AreEqual(result, "the brown <strong>(cow)!</strong> is not a dog");

      result = HighlightMatch.HighlightWord("the brown (cow)!* is not a dog", "(cow)!*", "strong");
      Assert.AreEqual(result, "the brown <strong>(cow)!*</strong> is not a dog");
    }


    [TestMethod]
    public void HighlighterWordPart()
    {
      string result = HighlightMatch.HighlightWordPart("the brown cow is not a dog", "co", "strong");
      Assert.AreEqual(result, "the brown <strong>co</strong>w is not a dog");

      result = HighlightMatch.HighlightWordPart("the browncow is not a dog", "co", "strong");
      Assert.AreEqual(result, "the brown<strong>co</strong>w is not a dog");

      result = HighlightMatch.HighlightWordPart("the browncow is not co a cog", "co", "strong");
      Assert.AreEqual(result, "the brown<strong>co</strong>w is not <strong>co</strong> a <strong>co</strong>g");

      result = HighlightMatch.HighlightWordPart("cococo co", "co", "strong");
      Assert.AreEqual(result, "<strong>co</strong><strong>co</strong><strong>co</strong> <strong>co</strong>");
    }


    [TestMethod]
    public void Highlighter2()
    {
      string result = HighlightMatch.HighlightPhrase("the brown cow is not a dog", "cow", "strong");
      Assert.AreEqual(result, "the brown <strong>cow</strong> is not a dog");

      result = HighlightMatch.HighlightPhrase("the browncow is not a dog", "cow", "strong");
      Assert.AreEqual(result, "the browncow is not a dog");

      result = HighlightMatch.HighlightPhrase("the browncow is not a dog", "the", "strong");
      Assert.AreEqual(result, "<strong>the</strong> browncow is not a dog");

      result = HighlightMatch.HighlightPhrase("the browncow is not a dog", "dog", "strong");
      Assert.AreEqual(result, "the browncow is not a <strong>dog</strong>");

      result = HighlightMatch.HighlightPhrase("the browncow is the not a dog the", "the", "strong");
      Assert.AreEqual(result, "<strong>the</strong> browncow is <strong>the</strong> not a dog <strong>the</strong>");

      result = HighlightMatch.HighlightPhrase("(the browncow is the ", "the", "strong");
      Assert.AreEqual(result, "<strong>(the</strong> browncow is <strong>the</strong>");

      result = HighlightMatch.HighlightPhrase("(the) browncow is the ", "the", "strong");
      Assert.AreEqual(result, "<strong>(the)</strong> browncow is <strong>the</strong>");
    }


    [TestMethod]
    public void Highlighter3()
    {
      string result = HighlightMatch.HighlightPhrase("the brown cow is not a dog", "cow the", "strong");
      Assert.AreEqual(result, "<strong>the</strong> brown <strong>cow</strong> is not a dog");

      result = HighlightMatch.HighlightPhrase("the brown cow is not a dog", "cow the", "strong");
      Assert.AreEqual(result, "<strong>the</strong> brown <strong>cow</strong> is not a dog");

      result = HighlightMatch.HighlightPhrase("the browncow is not a dog", "cow the", "strong");
      Assert.AreEqual(result, "<strong>the</strong> browncow is not a dog");

      result = HighlightMatch.HighlightPhrase("the brown cow is not a dog", "the brown dog", "strong");
      Assert.AreEqual(result, "<strong>the</strong> <strong>brown</strong> cow is not a <strong>dog</strong>");

      result = HighlightMatch.HighlightPhrase("the brown cow is not a dog cow cow cow", "the brown dog cow", "strong");
      Assert.AreEqual(result, "<strong>the</strong> <strong>brown</strong> <strong>cow</strong> is not a <strong>dog</strong> <strong>cow</strong> <strong>cow</strong> <strong>cow</strong>");
    }


    [TestMethod]
    public void WordsBeforeAndAfter1()
    {
      string str = HighlightMatch.WordsBeforeAndAfter("here is a match now", "match", 1);
      Assert.AreEqual(str, "a match now");

      str = HighlightMatch.WordsBeforeAndAfter("match", "match", 1);
      Assert.AreEqual(str, "match");

      str = HighlightMatch.WordsBeforeAndAfter("3 2 1 match 1 2 3", "match", 2);
      Assert.AreEqual(str, "2 1 match 1 2");

      str = HighlightMatch.WordsBeforeAndAfter("3 2 1 match 1 2 3", "match1", 2);
      Assert.AreEqual(str, "");

      str = HighlightMatch.WordsBeforeAndAfter("3 2 1 match. 1 2 3", "match", 2);
      Assert.AreEqual(str, "2 1 match. 1 2");

      str = HighlightMatch.WordsBeforeAndAfter("3 2 1 match.!() 1 2 3", "match", 2);
      Assert.AreEqual(str, "2 1 match.!() 1 2");

    }


    [TestMethod]
    public void WordsBeforeAndAfterPart()
    {
      string str = HighlightMatch.WordsBeforeAndAfterPart("here is a match now", "mat", 1);
      Assert.AreEqual(str, "a match now");

      str = HighlightMatch.WordsBeforeAndAfterPart("match", "match", 1);
      Assert.AreEqual(str, "match");

      str = HighlightMatch.WordsBeforeAndAfterPart("3 2 1 match 1 2 3", "mat", 2);
      Assert.AreEqual(str, "2 1 match 1 2");

      str = HighlightMatch.WordsBeforeAndAfterPart("3 2 1 match 1 2 3", "match1", 2);
      Assert.AreEqual(str, "");

      str = HighlightMatch.WordsBeforeAndAfterPart("3 2 1 match. 1 2 3", "mat", 2);
      Assert.AreEqual(str, "2 1 match. 1 2");

      str = HighlightMatch.WordsBeforeAndAfterPart("3 2 1 match.!() 1 2 3", "mat", 2);
      Assert.AreEqual(str, "2 1 match.!() 1 2");

    }


    [TestMethod]
    public void WordsBeforeAndAfter2()
    {
      string str = HighlightMatch.WordsBeforeAndAfter("here is a match now", "match", 1);
      Assert.AreEqual(str, "a match now");

      str = HighlightMatch.WordsBeforeAndAfter("match", "match", 1);
      Assert.AreEqual(str, "match");

      str = HighlightMatch.WordsBeforeAndAfter("3 2 1 match 1 2 3", "match", 2);
      Assert.AreEqual(str, "2 1 match 1 2");

      str = HighlightMatch.WordsBeforeAndAfter("3 2 1 match 1 2 3", "match1", 2);
      Assert.AreEqual(str, "");

      str = HighlightMatch.WordsBeforeAndAfter("so there. Senator agrees", "senator", 2);
      Assert.AreEqual(str, "so there. Senator agrees");

      str = HighlightMatch.WordsBeforeAndAfter("Federal election. Senator Cameron", "senator", 2);
      Assert.AreEqual(str, "Federal election. Senator Cameron");
    }


    [TestMethod]
    public void DotsBeforeAndAfter1()
    {
      string str = HighlightMatch.DotsBeforeAndAfter("here is a match now", "match", 2);
      Assert.AreEqual(str, "...is a match now");

      str = HighlightMatch.DotsBeforeAndAfter("word word here is a match now word word word", "match", 3);
      Assert.AreEqual(str, "...here is a match now word word...");

      str = HighlightMatch.DotsBeforeAndAfter("here is a match now word word", "match", 3);
      Assert.AreEqual(str, "here is a match now word word");

      str = HighlightMatch.DotsBeforeAndAfter("here is a match now word word word", "match", 3);
      Assert.AreEqual(str, "here is a match now word word...");

      str = HighlightMatch.DotsBeforeAndAfter("here is a match now word word word", "match1", 3);
      Assert.AreEqual(str, "");

      str = HighlightMatch.DotsBeforeAndAfter("here is a match now word word word", "", 3);
      Assert.AreEqual(str, "");

      str = HighlightMatch.DotsBeforeAndAfter("here is a match, now word word word", "match", 3);
      Assert.AreEqual(str, "here is a match, now word word...");

      str = HighlightMatch.DotsBeforeAndAfter("here is a match,!() now word word word", "match", 3);
      Assert.AreEqual(str, "here is a match,!() now word word...");
    }

    [TestMethod]
    public void WordMatch()
    {
      Assert.IsTrue(HighlightMatch.WordMatch("Amendment", "amendment"));
      Assert.IsFalse(HighlightMatch.WordMatch("Amendments", "amendment"));
      Assert.IsTrue(HighlightMatch.WordMatch("Amendment{", "amendment"));

      Assert.IsTrue(HighlightMatch.WordMatch("amendment", "(amendment"));
      Assert.IsTrue(HighlightMatch.WordMatch("amendment", "+amendment"));
      Assert.IsTrue(HighlightMatch.WordMatch("amendment", "(amendment?"));
    }


    [TestMethod]
    public void SearchResult()
    {
      string text = "Oh, don't speak to me of Austria. Perhaps I don't understand things, but Austria never has wished, and does not wish, for war.She is betraying us! Russia alone must save Europe.Our gracious sovereign recognizes his high vocation and will be true to it. That is the one thing I have faith in! Our good and wonderful sovereign has to perform the noblest role on earth, and he is so virtuous and noble that God will not forsake him. He will fulfill his vocation and crush the hydra of revolution, which has become more terrible than ever in the person of this murderer and villain! We alone must avenge the blood of the just one.... Whom, I ask you, can we rely on?... England with her commercial spirit will not and cannot understand the Emperor Alexander's loftiness of soul. She has refused to evacuate Malta. She wanted to find, and still seeks, some secret motive in our actions. What answer did Novosíltsev get? None. The English have not understood and cannot understand the self-abnegation of our Emperor who wants nothing for himself, but only desires the good of mankind.And what have they promised? Nothing! And what little they have promised they will not perform! Prussia has always declared that Buonaparte is invincible, and that all Europe is powerless before him.... And I don't believe a word that Hardenburg says, or Haugwitz either.This famous Prussian neutrality is just a trap.I have faith only in God and the lofty destiny of our adored monarch.He will save Europe!";

      Dictionary<string, string> matches = HighlightMatch.SearchResult(text, "Austria wished", 2, "strong");
      Assert.AreEqual(matches.Count, 2);
      Assert.AreEqual(matches["Austria"], "...me of <strong>Austria.</strong> Perhaps I...");
      Assert.AreEqual(matches["wished"], "...never has <strong>wished,</strong> and does...");
      matches.Clear();

      matches = HighlightMatch.SearchResult("Oh, don't speak to me of Austria", "Austria wished", 2, "strong");
      Assert.AreEqual(matches.Count, 1);
      Assert.AreEqual(matches["Austria"], "...me of <strong>Austria</strong>");

      matches = HighlightMatch.SearchResult("Oh, don't speak to me of Austria", "Oh", 2, "strong");
      Assert.AreEqual(matches.Count, 1);
      Assert.AreEqual(matches["Oh"], "<strong>Oh,</strong> don't speak...");

      matches = HighlightMatch.SearchResult("Oh, don't speak to me of Austria. Perhaps I don't understand things, but Austria never has wished, and does",
        "perhaps", 2, "strong");
      Assert.AreEqual(matches.Count, 1);
      Assert.AreEqual(matches["perhaps"], "...of Austria. <strong>Perhaps</strong> I don't...");

      matches = HighlightMatch.SearchResult("Oh, don't speak to me of Austria. Perhaps I don't understand things, but Austria never has wished, and does",
  "speak wish", 2, "strong");
      Assert.AreEqual(matches.Count, 1);
      Assert.AreEqual(matches["speak"], "Oh, don't <strong>speak</strong> to me...");
    }


    [TestMethod]
    public void SearchResultPartial()
    {
      string text = "Oh, don't speak to me of Austria. Perhaps I don't understand things, but Austria never has wished, and does not wish, for war.She is betraying us! Russia alone must save Europe.Our gracious sovereign recognizes his high vocation and will be true to it. That is the one thing I have faith in! Our good and wonderful sovereign has to perform the noblest role on earth, and he is so virtuous and noble that God will not forsake him. He will fulfill his vocation and crush the hydra of revolution, which has become more terrible than ever in the person of this murderer and villain! We alone must avenge the blood of the just one.... Whom, I ask you, can we rely on?... England with her commercial spirit will not and cannot understand the Emperor Alexander's loftiness of soul. She has refused to evacuate Malta. She wanted to find, and still seeks, some secret motive in our actions. What answer did Novosíltsev get? None. The English have not understood and cannot understand the self-abnegation of our Emperor who wants nothing for himself, but only desires the good of mankind.And what have they promised? Nothing! And what little they have promised they will not perform! Prussia has always declared that Buonaparte is invincible, and that all Europe is powerless before him.... And I don't believe a word that Hardenburg says, or Haugwitz either.This famous Prussian neutrality is just a trap.I have faith only in God and the lofty destiny of our adored monarch.He will save Europe!";

      Dictionary<string, string> matches = HighlightMatch.SearchResultPartial(text, "Aust wish", 2, "strong");
      Assert.AreEqual(matches.Count, 2);
      Assert.AreEqual(matches["Aust"], "...me of <strong>Aust</strong>ria. Perhaps I...");
      Assert.AreEqual(matches["wish"], "...never has <strong>wish</strong>ed, and does...");
      matches.Clear();

      matches = HighlightMatch.SearchResultPartial("Oh, don't speak to me of Austria", "Aust wished", 2, "strong");
      Assert.AreEqual(matches.Count, 1);
      Assert.AreEqual(matches["Aust"], "...me of <strong>Aust</strong>ria");

      matches = HighlightMatch.SearchResultPartial("Oh, don't speak to me of Austria", "Oh", 2, "strong");
      Assert.AreEqual(matches.Count, 1);
      Assert.AreEqual(matches["Oh"], "<strong>Oh</strong>, don't speak...");

      matches = HighlightMatch.SearchResultPartial("Oh, don't speak to me of Austria. Perhaps I don't understand things, but Austria never has wished, and does",
        "perhaps", 2, "strong");
      Assert.AreEqual(matches.Count, 1);
      Assert.AreEqual(matches["perhaps"], "...of Austria. <strong>Perhaps</strong> I don't...");
    }


    [TestMethod]
    public void SearchResultFastPhrase()
    {
      string text = "Oh, don't speak to me of Austria. Perhaps I don't understand things, but Austria never has wished, and does not wish, for war.She is betraying us! Russia alone must save Europe.Our gracious sovereign recognizes his high vocation and will be true to it. That is the one thing I have faith in! Our good and wonderful sovereign has to perform the noblest role on earth, and he is so virtuous and noble that God will not forsake him. He will fulfill his vocation and crush the hydra of revolution, which has become more terrible than ever in the person of this murderer and villain! We alone must avenge the blood of the just one.... Whom, I ask you, can we rely on?... England with her commercial spirit will not and cannot understand the Emperor Alexander's loftiness of soul. She has refused to evacuate Malta. She wanted to find, and still seeks, some secret motive in our actions. What answer did Novosíltsev get? None. The English have not understood and cannot understand the self-abnegation of our Emperor who wants nothing for himself, but only desires the good of mankind.And what have they promised? Nothing! And what little they have promised they will not perform! Prussia has always declared that Buonaparte is invincible, and that all Europe is powerless before him.... And I don't believe a word that Hardenburg says, or Haugwitz either.This famous Prussian neutrality is just a trap.I have faith only in God and the lofty destiny of our adored monarch.He will save Europe!";

      string matches = HighlightMatch.SearchResultFastPhraseFormatted(text, "Austria wish", "strong", 20);
      Assert.AreEqual(matches, "<p>..on't speak to me of <strong>Austria</strong>. Perhaps I don't un..</p><p>..ished, and does not <strong>wish</strong>, for war.She is bet..</p>");

      matches = HighlightMatch.SearchResultFastPhraseFormatted(text, "Austria perhaps", "strong", 20);
      Assert.AreEqual(matches, "<p>..on't speak to me of <strong>Austria</strong>. <strong>Perhaps</strong> I don't un..</p>");

      text = " Austria perhaps ";
      matches = HighlightMatch.SearchResultFastPhraseFormatted(text, "Austria perhaps", "strong", 20);
      Assert.AreEqual(matches, "<p>.. <strong>Austria</strong> <strong>perhaps</strong> ..</p>");

      text = "Austria perhaps";
      matches = HighlightMatch.SearchResultFastPhraseFormatted(text, "Austria perhaps", "strong", 20);
      Assert.AreEqual(matches, "<p>..<strong>Austria</strong> <strong>perhaps</strong>..</p>");

      //text = "Scheduling and Ahead Markets Submission to the ESB Consultation Paper Creative Energy Consulting Pty Ltd October 2020 EXECUTIVE SUMMARY OVERVIEW The Australian Energy Council has engaged Creative Energy Consulting to prepare a submission, on the area of scheduling and ahead markets, to the ESBs recent post - 2025 NEM design consultation paper. This follows on from an earlier engagement in this area, which culminated in a paper that was submitted to the ESB in June.This submission draws on that June papers framing and analysis of the issues, applying that thematic structure to the ESBs latest proposals.There are some welcome developments in the latest ESB papers. In particular, design options involving mandatory ahead market participation have been ruled out, and some more detail around the ahead market design has been developed and presented. However, the papers have still not satisfactorily answered the basic questions posed in our June paper: what specific problems are seen to be emerging with the current scheduling process; how an ahead market would address these; and why other potential options are not being explored.Building on these generic questions, five specific areas of concern arising in the new ESB papers are identified and discussed in this submission: 1.Possible reforms to pre - dispatch have not been discussed 2.A voluntary, net ahead market cannot perform a scheduling role 3.The UCS scheduling principles remain unclear; 4.The ahead market should not schedule non-market ancillary services 5.The value of ahead hedging is low.These are explained further below and discussed in detail in the main body of this paper.PRE - DISPATCH REFORM NOT DISCUSSED Our June paper described in detail the existing design of the pre - dispatch process and its role in the scheduling and commitment of generation over the ahead timescale. It also presented some ideas for reforms to this process that might be considered.The consultation paper acknowledges the former but has ignored the latter.The entire focus of the paper continues";
      //matches = Highlighter.SearchResultFastPhraseFormatted(text, "NEW design consultation", "strong", 20);
      //Assert.AreEqual(matches, "<p>..<strong>Austria</strong> <strong>perhaps</strong>..</p>");
    }


    [TestMethod]
    public void SearchResultFastWord()
    {
      string text = "Oh Austria Perhaps";

      string match = HighlightMatch.SearchResultFastWord(text, "Austria", "strong", 2);
      Assert.AreEqual(match, "h <strong>Austria</strong> P");

      match = HighlightMatch.SearchResultFastWord(text, "Austria", "strong", 20);
      Assert.AreEqual(match, "Oh <strong>Austria</strong> Perhaps");

      match = HighlightMatch.SearchResultFastWord(text, "Austria", "strong", 8);
      Assert.AreEqual(match, "Oh <strong>Austria</strong> Perhaps");

      match = HighlightMatch.SearchResultFastWord(text, "Austria", "strong", 7);
      Assert.AreEqual(match, "Oh <strong>Austria</strong> Perhap");

      match = HighlightMatch.SearchResultFastWord("AustrIA", "Austria", "strong", 70);
      Assert.AreEqual(match, "<strong>AustrIA</strong>");

      match = HighlightMatch.SearchResultFastWord("AustrIA", "Austria", "strong", 0);
      Assert.AreEqual(match, "<strong>AustrIA</strong>");
    }


    [TestMethod]
    public void TerminateTests()
    {
      Assert.AreEqual("is a sentence", HighlightMatch.TerminateIncompleteWords("here is a sentence now"));
      Assert.AreEqual("sentence", HighlightMatch.TerminateIncompleteWords("sentence"));
      Assert.AreEqual("sentence", HighlightMatch.TerminateIncompleteWords(" sentence "));
      Assert.AreEqual("sentence", HighlightMatch.TerminateIncompleteWords("a sentence b"));
      Assert.AreEqual("", HighlightMatch.TerminateIncompleteWords(""));
      Assert.AreEqual("abcd", HighlightMatch.TerminateIncompleteWords("abcd ef"));
    }


  }
}

