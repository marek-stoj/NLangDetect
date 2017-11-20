using System.Collections.Generic;
using NUnit.Framework;

namespace NLangDetect.Core.Tests
{
  [TestFixture]
  public class LanguageDetectorTests
  {
    [OneTimeSetUp]
    public void TestFixtureSetUp()
    {
      LanguageDetector.Initialize("LangProfiles");
    }

    [OneTimeTearDown]
    public void TestFixtureTearDown()
    {
      LanguageDetector.Release();
    }

    [Test]
    [TestCaseSource("TestCases_for_DetectLanguage_correctly_detects_language")]
    public void DetectLanguage_correctly_detects_language(string textSample, LanguageName expectedLanguageName)
    {
      LanguageName? detectedLanguageName =
        LanguageDetector.DetectLanguage(textSample);

      Assert.AreEqual(expectedLanguageName, detectedLanguageName);
    }

    // ReSharper disable UnusedMethodReturnValue.Local

    private static IEnumerable<TestCaseData> TestCases_for_DetectLanguage_correctly_detects_language()
    {
      LanguageName languageName;

      // ----------------------------------------------------------------------------------------
      languageName = LanguageName.En;

      yield return
        new TestCaseData(
          "That's when I started to get a little freaked out about the math. Perhaps we can throw 5% of the entrants out as obviously incomplete or unfinished. That leaves 323 entries to judge. Personally, I'm not comfortable saying I judged a competition unless I actually look at each one of the entries, so at an absolute minimum I have to click through to each webapp. Once I do, I couldn't imagine properly evaluating the webapp without spending at least 30 seconds looking at the homepage.  Let's be generous and say I need 10 seconds to orient myself and account for page load times, and 30 seconds to look at each entry. That totals three and a half hours of my, y'know, infinitely valuable time. In which I could be finding a cure for cancer, or clicking on LOLcats. I still felt guilty about only allocating half a minute per entry; is it fair to the contestants if I make my decision based on 30 seconds of scanning their landing page and maybe a few desultory clicks?  But then I had an epiphany: yes, deciding in 30 seconds is totally completely unfair, but that's also exactly how it works in the real world. Users are going to click through to your web site, look at it for maybe 30 seconds, and either decide that it's worthy, or reach for the almighty back button on their browser and bug out. Thirty seconds might even be a bit generous. In one Canadian study, users made up their mind about websites in under a second.",
          languageName).SetName(languageName.ToString());

      // ----------------------------------------------------------------------------------------
      languageName = LanguageName.Pl;

      yield return
        new TestCaseData(
          "Jeśli podchodzisz do tego w niewłaściwy sposób, gimnastyka jest trudna, nudna i wymaga zbyt wiele pracy. Również zdrowe jedzenie, jeśli źle do tego podchodzisz, okazuje się wymagać za dużo dyscypliny. Z tego powodu ludzie (włączając mnie jeszcze kilka lat temu) często robią niezdrowe rzeczy, ponieważ tak jest łatwiej i zabawniej. Łatwo jest nie ćwiczyć i zamiast tego oglądać telewizję lub surfować po Internecie. Więcej zabawy sprawia jedzenie nachos lub frytek, smażonego kurczaka lub słodyczy.",
          languageName).SetName(languageName.ToString());

      // ----------------------------------------------------------------------------------------
      languageName = LanguageName.De;

      yield return
        new TestCaseData(
          "Die Abgeschlossenheitsbescheinigung (AB) ist nach deutschem Recht eine Bescheinigung darüber, dass eine Eigentumswohnung oder ein Teileigentum im Sinne des Wohnungseigentumsgesetzes (WEG) baulich hinreichend von anderen Wohnungen und Räumen abgeschlossen ist (§ 3 Abs. 2, § 7 Abs. 4 Nr. 2 WEG). Diese Trennung erfolgt beispielsweise durch Wände und Decken, die den Schall- und Wärmeschutz gewährleisten. Es muss weiterhin ein eigener, abschließbarer Zugang zu jeder Einheit vorhanden sein.",
          languageName).SetName(languageName.ToString());

      // ----------------------------------------------------------------------------------------
      languageName = LanguageName.Fr;

      yield return
        new TestCaseData(
          "C'est alors que j'ai commencé à avoir un peu flippé sur le calcul. Peut-être que nous pouvons jeter 5% des entrants comme manifestement incomplet ou non fini. Cela laisse 323 entrées pour juger. Personnellement, je ne suis pas à l'aise en disant que je juge un concours, sauf si j'ai vraiment regarder chacune des entrées, donc à un minimum absolu, je dois cliquer sur chaque webapp. Une fois que je fais, je ne pouvais pas imaginer évaluer correctement la webapp sans dépenser au moins 30 secondes en regardant la page d'accueil. Soyons généreux et disons que je besoin de 10 secondes pour m'orienter et tenir compte des temps de chargement des pages, et 30 secondes à regarder chaque entrée. Qui totalise trois ans et demi heures de mon infini, tu sais, un temps précieux. Dans lequel je pourrais être de trouver un remède contre le cancer, ou en cliquant sur LOLcats. J'ai toujours senti coupable de ne allouant une demi-minute par entrée, est-ce juste pour les concurrents si je prends ma décision sur la base de 30 secondes de la numérisation de leur page de destination et peut-être quelques clics décousues? Mais ensuite, j'ai eu une révélation: oui, décider en 30 secondes est totalement totalement injuste, mais c'est aussi exactement comment il fonctionne dans le monde réel. Les utilisateurs vont cliquer sur votre site web, regarder pendant peut-être 30 secondes, soit décider qu'il est digne, ou pour atteindre le bouton de retour tout-puissant sur ​​leur navigateur et bug sur. Trente secondes pourrait même être un peu plus généreux. Dans une étude canadienne, les utilisateurs pris leur décision sur les sites Web en moins d'une seconde.",
          languageName).SetName(languageName.ToString());

      // ----------------------------------------------------------------------------------------
      languageName = LanguageName.Es;

      yield return
        new TestCaseData(
          "Fue entonces cuando empecé a ponerme un poco asustado por las matemáticas. Tal vez podamos lanzar un 5% de los participantes fuera tan obviamente incompleto o sin terminar. Eso deja a 323 entradas de juzgar. Personalmente, no me siento cómodo diciendo que ha considerado una competencia a menos que realmente se ven en cada una de las entradas, por lo menos un mínimo absoluto tengo que hacer clic a través de cada webapp. Una vez que lo hago, no me podía imaginar correctamente la evaluación de la aplicación web sin tener que gastar al menos 30 segundos mirando la página de inicio. Seamos generosos y digamos que necesito 10 segundos para orientarme y dar cuenta de los tiempos de carga de páginas, y 30 segundos para ver cada entrada. Eso asciende a tres horas y media de mi, ya sabes, infinitamente valioso tiempo. En lo que podría ser la búsqueda de una cura para el cáncer, o haciendo clic en LOLcats. Todavía me sentía culpable por sólo la asignación de medio minuto por cada entrada, ¿es justo para los concursantes si hago mi decisión basada en 30 segundos de barrido su página de destino y tal vez unos pocos clics inconexas? Pero entonces tuve una epifanía: sí, decidiendo en 30 segundos es totalmente completamente injusto, pero eso es también exactamente cómo funciona en el mundo real. Los usuarios a hacer clic a través de su sitio web, mire por tal vez 30 segundos, y, o bien decidir que es digno, o alcanzar el botón Atrás de su navegador todopoderoso y bichos fuera. Treinta segundos, incluso podría ser un poco generoso. En un estudio canadiense, los usuarios compone su mente acerca de sitios web en menos de un segundo.",
          languageName).SetName(languageName.ToString());

      // ----------------------------------------------------------------------------------------
      languageName = LanguageName.Zh_Cn;

      yield return
        new TestCaseData(
          "这时候，我开始有点吓坏了的数学。也许我们可以抛出5％的参赛者显然是不完整或未完成的。这使得323项的判断。就个人而言，我说，我的判断的竞争，除非我其实看的每一个项目，所以在绝对最低限度，我要点击到每个web应用不舒服。一旦我做什么，我想象不出正确评估的web应用程序无需花费至少30秒，在首页。让我们可以大方的说，我需要10秒定位自己和帐户的页面加载时间，30秒，在每个条目。总计三个半小时，你知道，我的无限宝贵的时间。我在其中可以找到治疗癌症的方法，或点击LOLcats。我仍然感到内疚，大约只有半分钟每个条目分配是公平的选手，如果我做我的决定是基于30秒的扫描他们的登陆页面，也许有一些散漫的点击？但是当时我有一种顿悟：是的，在30秒内决定是完全完全不公平的，但是这也是在现实世界中，究竟它是如何工作的。用户要点击到你的网站，看看它或许是30秒，并且决定了它是值得的，或达到回全能的浏览器上的按钮和错误了。三十秒钟，甚至可能是有点大方。用户在一个加拿大的研究，取得了他们的头脑在一秒钟之约网站。",
          languageName).SetName(languageName.ToString());

      // ----------------------------------------------------------------------------------------
      languageName = LanguageName.Zh_Tw;

      yield return
        new TestCaseData(
          "以下的文章中，你或許會讀到許多程式設計的專業術語，但是並未以非程式設計師可以理解的程度來解釋這些細節。以下的文章中，你或許會讀到許多程式設計的專業術語，但是並未以非程式設計師可以理解的程度來解釋這些細節。以下的文章中，你或許會讀到許多程式設計的專業術語，但是並未以非程式設計師可以理解的程度來解釋這些細節。以下的文章中，你或許會讀到許多程式設計的專業術語，但是並未以非程式設計師可以理解的程度來解釋這些細節。以下的文章中，你或許會讀到許多程式設計的專業術語，但是並未以非程式設計師可以理解的程度來解釋這些細節。",
          languageName).SetName(languageName.ToString());

      // ----------------------------------------------------------------------------------------
      languageName = LanguageName.Ru;

      yield return
        new TestCaseData(
          "Вот когда я начал, чтобы получить немного волновался о математике. Может быть, мы можем бросить 5% от абитуриентов, как, очевидно, неполной или незаконченным. Это оставляет 323 записей для судьи. Лично я не удобно сказать, что я судил конкурс, если я на самом деле смотрят друг на одной из записей, поэтому на абсолютный минимум я должен перейти на каждом веб-приложение. Как только я делаю, я не мог себе представить должным образом оценить веб-приложение, не тратя по крайней мере, 30 секунд смотрите на главной странице. Давайте быть щедрым и говорят мне нужно 10 секунд, чтобы сориентироваться и учитывать время загрузки страницы, и 30 секунд смотреть на каждую запись. , Что составляет три с половиной часа моя, ты знаешь, бесконечно драгоценное время. В котором я мог бы найти лекарство от рака, или нажав на LOLcats. Я все еще чувствовал себя виноватым только о выделении полминуты на запись; это справедливо по отношению к участникам, если я принимаю решение на основе 30 секунд сканирования их целевой страницы и, возможно, несколько отрывочных кликов? Но тогда у меня было прозрение: да, решая за 30 секунд полностью совершенно несправедливо, но это также точно, как это работает в реальном мире. Пользователи собираются по ссылке на ваш сайт, посмотрите на него, может быть, 30 секунд, и либо решит, что она достойна, или дотянуться до кнопки всемогущий обратно на свой ​​браузер и ошибка из. Тридцать секунд даже может быть немного щедр. В одном канадском исследовании, пользователи составили свое мнение о веб-сайтах в рамках второго.",
          languageName).SetName(languageName.ToString());

      languageName = LanguageName.Ru;

      // ----------------------------------------------------------------------------------------
      yield return
        new TestCaseData(
          "Вот когда я начал, чтобы получить немного волновался о математике. Может быть, мы можем бросить 5% от абитуриентов, как, очевидно, неполной или незаконченным. Это оставляет 323 записей для судьи. Лично я не удобно сказать, что я судил конкурс, если я на самом деле смотрят друг на одной из записей, поэтому на абсолютный минимум я должен перейти на каждом веб-приложение. Как только я делаю, я не мог себе представить должным образом оценить веб-приложение, не тратя по крайней мере, 30 секунд смотрите на главной странице. Давайте быть щедрым и говорят мне нужно 10 секунд, чтобы сориентироваться и учитывать время загрузки страницы, и 30 секунд смотреть на каждую запись. , Что составляет три с половиной часа моя, ты знаешь, бесконечно драгоценное время. В котором я мог бы найти лекарство от рака, или нажав на LOLcats. Я все еще чувствовал себя виноватым только о выделении полминуты на запись; это справедливо по отношению к участникам, если я принимаю решение на основе 30 секунд сканирования их целевой страницы и, возможно, несколько отрывочных кликов? Но тогда у меня было прозрение: да, решая за 30 секунд полностью совершенно несправедливо, но это также точно, как это работает в реальном мире. Пользователи собираются по ссылке на ваш сайт, посмотрите на него, может быть, 30 секунд, и либо решит, что она достойна, или дотянуться до кнопки всемогущий обратно на свой ​​браузер и ошибка из. Тридцать секунд даже может быть немного щедр. В одном канадском исследовании, пользователи составили свое мнение о веб-сайтах в рамках второго.",
          languageName).SetName(languageName.ToString());

      // ----------------------------------------------------------------------------------------
      languageName = LanguageName.Bg;

      yield return
        new TestCaseData(
          "Това е, когато аз започнах да се получи малко откачи за математика. Може би ще хвърлят 5% от участници като очевидно непълно или незавършени. Това оставя 323 вписвания за съдия. Лично на мен не ми е удобно да кажа, че съди конкурс, освен ако аз действително изглежда на всеки един от текстовете, така че в абсолютен минимум, трябва да кликнете до всеки уеб приложение. След като го направя, аз не можех да си представя оценка на уеб приложение, без да харчите по-малко от 30 секунди в началната страница. Нека бъдем щедри и казват, че се нуждаят от 10 секунди, за да се ориентират себе си и за времето за зареждане на страницата, и 30 секунди, за да гледам при всяко влизане. Това възлиза на три и половина часа на моя, нали се сещате, безкрайно ценно време. , В която мога да се намери лек за рака, или кликнете върху котки LOL. Все още се чувствах виновен само разпределяне на половин минута на влизане, е справедливо на състезателите, ако направя моето решение, въз основа на 30 секунди за сканиране на тяхната целева страница, а може би и няколко откъслечни кликвания? Но тогава имах просветление \": да, да вземе решение в рамките на 30 секунди е напълно несправедливо, но това е също така, как точно работи в реалния свят. Потребителите ще чрез кликване към вашия уеб сайт, гледам на него може би 30 секунди, и реши, че е достоен, или достигне бутона всемогъщ гръб на браузъра си и бъгове,. Трийсет секунди може дори да бъде малко щедър. В едно канадско проучване, потребителите мнението си за сайтове под секунда.",
          languageName).SetName(languageName.ToString());

      // ----------------------------------------------------------------------------------------
      languageName = LanguageName.Bn;

      yield return
        new TestCaseData(
          "এটা যখন আমি যাও একটু গণিত সম্পর্কে বিচিত্রভাবে ছোপ - লাগানো বা ডোরাকাটা আউট পেতে শুরু করে. সম্ভবত আমরা সম্ভবত অসম্পূর্ণ বা অসমাপ্ত হিসাবে সমাগম 5% নিক্ষেপ করে দেখতে পারেন. যে বিচারক 323 থেকে পাতা. ব্যক্তিগতভাবে, আমি আরামদায়ক বলছে আমি একটি প্রতিযোগিতার গণ্য হবে না যদি না আমি আসলে থেকে প্রতিটি সময়ে, চেহারা যাতে একটি পরম সর্বনিম্ন সময়ে আমি প্রতিটি webapp মাধ্যমে ক্লিক করতে হবে না. একবার আমি, আমি সঠিকভাবে ব্যয় ছাড়া webapp মূল্যায়নের অন্তত 30 হোমপেজে যাও আমি কল্পনা করতে পারি নি. চলুন শুরু করা যাক এবং উদার হতে বলতে আমি প্রাচী নিজেকে এবং পৃষ্ঠা লোড বার, এবং 30 সেকেন্ডের জন্য অ্যাকাউন্টে 10 সেকেন্ডের প্রতিটি এন্ট্রি তাকান প্রয়োজন. যে আমার তিনটি এবং একটি অর্ধ ঘন্টা, y'know, অসীম মূল্যবান সময় সমগ্র. যা আমি ক্যান্সারের জন্য একটি নিরাময়, ফাইন্ডিং হতে পারে অথবা LOLcats ক্লিক করুন. আমি এখনও অনুভূত কেবলমাত্র অর্ধেক এন্ট্রি প্রতি মিনিট বণ্টন সম্পর্কে দোষী; এটা পরিষ্কার প্রতিযোগী যাও যদি আমি না আমার সিদ্ধান্ত তাদের অবতরণ পাতা স্ক্যানিং 30 সেকেন্ড এবং হয়ত কয়েক অসংলগ্ন ক্লিকের উপর ভিত্তি করে? কিন্তু তারপর আমি একটি Epiphany বলেছেন: হ্যাঁ, 30 সেকেন্ডের মধ্যে সিদ্ধান্ত হচ্ছে, তা সম্পূর্ণই সম্পূর্ণ অন্যায্য, কিন্তু যে এর ঠিক কিভাবে এটা বাস্তব জগতে কাজ করে. ব্যবহারকারীরা আপনার ওয়েব সাইট এর মাধ্যমে ক্লিক যাচ্ছে এ, হয়তো 30 সেকেন্ডের জন্য চেহারা, হয় এবং যে যোগ্য সিদ্ধান্ত নেন, বা তাদের ব্রাউজার এবং বাগ আউট উপর সর্বশক্তিমান ব্যাক বোতাম পৌঁছানোর জন্য. থার্টি যাও এমনকি হতে একটু উদার হতে পারে. এক কানাডিয়ান স্টাডিতে ব্যবহারকারীদের ওয়েবসাইটের সম্পর্কে অধীন একটি দ্বিতীয় তাদের মন আপ.",
          languageName).SetName(languageName.ToString());

      // ----------------------------------------------------------------------------------------
      languageName = LanguageName.It;

      yield return
        new TestCaseData(
          "In quel momento ho iniziato ad avere un po 'spaventata per la matematica. Forse si può buttare il 5% dei partecipanti come, ovviamente, incompleto o non finito. Che lascia 323 voci di giudicare. Personalmente, non mi sento a mio agio dicendo che ho giudicato un concorso a meno che in realtà guardiamo una delle voci, quindi al minimo assoluto devo scegliere tramite per ogni webapp. Una volta che lo faccio, non potevo immaginare correttamente valutare la webapp senza spendere almeno 30 secondi guardando la homepage. Cerchiamo di essere generosi e dire che ho bisogno di 10 secondi per orientarmi e considerazione per i tempi di caricamento delle pagine, e 30 secondi a guardare ogni voce. Che ammonta a tre ore e mezza del mio, sai, infinitamente tempo prezioso. In che ho potuto essere trovare una cura per il cancro, o facendo clic su Gattini. Mi sentivo ancora in colpa per l'assegnazione solo mezzo minuto per ogni voce, è giusto per i concorrenti se faccio la mia decisione sulla base di 30 secondi di scansione loro pagina di destinazione e forse un paio di click saltuari? Ma poi ho avuto una rivelazione: sì, decidendo in 30 secondi è totalmente ingiusto, ma che è anche esattamente come funziona nel mondo reale. Ci sono utenti che andando a cliccare per accedere al tuo sito web, guardare per forse 30 secondi, o decidere che è degno, o raggiungere per il pulsante onnipotente in merito alla loro browser e bug fuori. Trenta secondi potrebbe anche essere un po 'generoso. In uno studio canadese, gli utenti di fatto la loro mente su siti web in meno di un secondo.",
          languageName).SetName(languageName.ToString());

      // ----------------------------------------------------------------------------------------
      languageName = LanguageName.Ja;

      yield return
        new TestCaseData(
          "私は数学についてびびる少しを得るために始めたときだ。おそらく、我々は明らかに不完全であるかのように未完成の参入の5％を捨てることができます。それは裁判官に323のエントリを残します。個人的には、私は実際に私が各Webアプリケーションに至るまでクリックしなければならない絶対的な最低限ので、エントリの各1を見ていない限り、私は競争を判断したと言って快適ではないよ。私は何をしたら、ちゃんとホームページを見て、少なくとも30秒費やすことなく、Webアプリケーションを評価する想像できませんでした。レッツは、寛大であると私は、各エントリを見て自分の向き、ページの読み込み時間、および30秒間のアカウントに10秒を必要とします。それは私の3時間半、削ら、無限に貴重な時間を合計します。これで私は、がんの治療法を見つけることができ、またはLOLcatsをクリック。私はまだ唯一のエントリごとに30秒を配分に関する罪の意識を感じていた、私は私の決定は、30彼らのランディングページをスキャンしてから数秒と、おそらくいくつかのとりとめのないクリック数に基づいて行う場合、それは出場者に公正なのですか？しかし、私はひらめきを持っていた：はい、30秒以内に決定することは完全に完全に不公平であるが、それはそれが現実の世界で働いて正確にどのようでもあります。ユーザーが自分のWebサイトへのクリックスルーしようとしている、多分30秒のためにそれを見て、どちらかそれは立派だと判断したか、それらのブラウザとバグアウトに全能の戻るボタンに手を伸ばす。三十秒も少し寛大かもしれない。 1カナダの研究では、ユーザーが第二の下にウェブサイトについての彼らの決心をした。",
          languageName).SetName(languageName.ToString());

      // ----------------------------------------------------------------------------------------
      languageName = LanguageName.Ko;

      yield return
        new TestCaseData(
          "나는 수학에 대해 좀 흥분한를 얻을 때부터입니다. 아마도 우리는 분명 불완전하거나 미완성으로 참가자의 5 %를 던질 수 있습니다. 그 판사에게 323 항목을 출발합니다. 개인적으로, 나는 사실 각 webapp에 이르기까지 클릭 할 필요 절대 최소한 있으므로 항목의 각 한 곳에서 볼하지 않는 한 나는 경쟁을 판단 말을 불편 해요. 그렇게되면, 제대로 홈페이지를보고 최소 30 초 지출없이 webapp을 평가 상상할 수 없습니다. 하자 관대하고 각 항목 보는 방향 자신과 페이지로드 시간, 30 초 동안 계정에 10 초 필요 말한다. 그건 내 세 시간 반, 에서요, 무한 귀중한 시간을 합계. 어떤 난 암에 대한 치료법을 찾는 수, 또는 LOLcats을 클릭합니다. 아직도에만 항목에 반 분을 할당 대해 죄책감을 느끼는, 나는 내 결정 30의 방문 페이지를 스캔의 초 어쩌면 몇 일관성없는 클릭 수를 기준으로하는 경우는 참가자에게 공정한입니까? 하지만 난 변화를 일으켰다 : 예, 30 초 만에 결정하는 것은 완전히, 완벽히 불공평하지만은 현실 세계에서 작동하는 방법을 정확하게도 있습니다. 사용자가 웹 사이트를 통해 클릭 할 거예요, 아마 30 초 동안 좀 봐 있으며이 맞는 것을 결정하거나 브라우저 및 버그 아웃에 전능하신 뒤로 버튼에 도달합니다. 30 초는 조금 넉넉하게 할 수 있습니다. 한 캐나다 연구에서, 사용자는 두 번째 이하의 웹 사이트에 대한 자신의 마음을했습니다.",
          languageName).SetName(languageName.ToString());

      // ----------------------------------------------------------------------------------------
      languageName = LanguageName.Pt;

      yield return
        new TestCaseData(
          "Foi quando eu comecei a ficar um pouco assustado com a matemática. Talvez possamos jogar 5% dos participantes se como obviamente incompletos ou inacabados. Isso deixa 323 entradas para julgar. Pessoalmente, eu não me sinto confortável dizendo julguei uma competição a menos que eu realmente olhar para cada uma das entradas, portanto, em um mínimo absoluto eu tenho que clicar para cada webapp. Uma vez que eu faço, eu não poderia imaginar corretamente avaliar o webapp sem gastar pelo menos 30 segundos olhando para a página inicial. Vamos ser generosos e dizer que precisa de 10 segundos para me orientar e conta para os tempos de carga de páginas, e 30 segundos para olhar para cada entrada. Que totaliza três horas e meia da minha, você sabe, infinitamente precioso tempo. Em que eu poderia ser encontrar uma cura para o câncer, ou clicar em LOLcats. Eu ainda me sentia culpada só alocação de meio minuto por entrada; é justo para os competidores se faço a minha decisão com base em 30 segundos de digitalização de sua página de destino e talvez alguns cliques desconexos? Mas então eu tive uma epifania: sim, decidir em 30 segundos é totalmente completamente injusto, mas que também é exatamente como ele funciona no mundo real. Usuários vão clicar no seu site, olha para talvez 30 segundos, e quer decidir que é digno, ou alcançar o botão de volta todo-poderoso em seu navegador e bug fora. Trinta segundos pode até ser um pouco generoso. Em um estudo canadense, os usuários fizeram a sua mente sobre sites em menos de um segundo.",
          languageName).SetName(languageName.ToString());

      // ----------------------------------------------------------------------------------------
      languageName = LanguageName.Sv;

      yield return
        new TestCaseData(
          "Det var då jag började bli lite freaked om matematik. Kanske vi kan kasta 5% av de tävlande ut som uppenbart ofullständiga eller oavslutade. Då återstår 323 poster att döma. Personligen är jag inte bekväm att jag bedömde en tävling om jag faktiskt titta på var och en av posterna, så på ett absolut minimum måste jag klicka sig vidare till respektive webapp. När jag gör det, kunde jag inte tänka mig ordentligt utvärdera webapp utan utgifterna minst 30 sekunder tittar på hemsidan. Låt oss vara generösa och säga att jag behöver 10 sekunder för att orientera mig och redovisa tider sidan laddas, och 30 sekunder för att titta på varje post. Det uppgår tre och en halv timmar av mitt, du vet, oändligt värdefull tid. Där jag kan hitta ett botemedel mot cancer, eller klicka på lolcats. Jag kände mig fortfarande skyldig om endast allokera en halv minut per post, är det rimligt att de tävlande om jag gör mitt beslut baserat på 30 sekunder skanna deras målsida och kanske några osammanhängande klick? Men sedan hade jag en uppenbarelse: ja, besluta på 30 sekunder är helt helt orättvist, men det är också exakt hur det fungerar i verkligheten. Användarna kommer att klicka sig vidare till din webbplats, titta på det för kanske 30 sekunder, och antingen besluta att det är värt, eller nå för den allsmäktige bakåtknappen på sina webbläsare och bugg ut. Trettio sekunder kanske vara lite generösa. I en kanadensisk studie, gjorde användarna upp uppfattning om webbplatser i under en sekund.",
          languageName).SetName(languageName.ToString());

      // ----------------------------------------------------------------------------------------
      languageName = LanguageName.Kn;

      yield return
        new TestCaseData(
          "ನಾನು ಗಣಿತದ ಬಗ್ಗೆ ಪ್ರೀಕ್ಡ್ ಸ್ವಲ್ಪ ಪಡೆಯಲು ಆರಂಭಿಸಿದಾಗ ಆ. ಬಹುಶಃ ನಾವು ನಿಸ್ಸಂಶಯವಾಗಿ ಅಪೂರ್ಣ ಅಥವಾ ಅಪೂರ್ಣ ಎಂದು ತಂಡಗಳ 5% ವಿಸರ್ಜಿತವಾದುದು ಮಾಡಬಹುದು. ಎಂದು ನ್ಯಾಯಾಧೀಶರಿಗೆ 323 ನಮೂದುಗಳನ್ನು ಎಲೆಗಳು. ವೈಯಕ್ತಿಕವಾಗಿ, ನಾನು ನಾನು ಪ್ರತಿ webapp ಮೂಲಕ ಕ್ಲಿಕ್ ಮಾಡಬೇಕಾಗಬಹುದು ಒಂದು ಪರಿಪೂರ್ಣವಾದ ಕನಿಷ್ಠ ಆದ್ದರಿಂದ, ನಮೂದುಗಳನ್ನು ಪ್ರತಿಯೊಂದು ನೋಡಲು ಹೊರತು ನಾನು ಸ್ಪರ್ಧೆಯಲ್ಲಿ ತೀರ್ಮಾನಿಸಲಾಗುತ್ತದೆ ಹೇಳುವ ಆರಾಮದಾಯಕ ಅಲ್ಲ. ನಾನು ಒಮ್ಮೆ, ನಾನು ಸರಿಯಾಗಿ ಮುಖಪುಟಕ್ಕೆ ನೋಡುವುದರಿಂದ ಕನಿಷ್ಠ 30 ಸೆಕೆಂಡುಗಳ ವ್ಯಯಿಸದೇ webapp ಮೌಲ್ಯಮಾಪನ ಊಹಿಸಿಕೊಳ್ಳಲು ಸಾಧ್ಯವಿಲ್ಲ. ಲೆಟ್ಸ್ ಉದಾರ ಮತ್ತು ನಾನು ಪ್ರತಿಯೊಂದು ನಮೂದು ನೋಡಲು ಓರಿಯಂಟ್ ನನ್ನ ಮತ್ತು ಪುಟ ಲೋಡ್ ಬಾರಿ, ಮತ್ತು 30 ಸೆಕೆಂಡುಗಳ ಕಾಲ ಖಾತೆಗೆ 10 ಸೆಕೆಂಡುಗಳ ಅಗತ್ಯವಿದೆ ಹೇಳುತ್ತಾರೆ. ನನ್ನ ಮೂರು ಮತ್ತು ಒಂದು ಅರ್ಧ ಗಂಟೆಗಳ y'know, ಕೊನೆಯಿಲ್ಲದ ಅಮೂಲ್ಯ ಸಮಯ ಮೊತ್ತವನ್ನು ಒಟ್ಟು ಮೊತ್ತ. ಇದರಲ್ಲಿ ನಾನು ಕ್ಯಾನ್ಸರ್ ಚಿಕಿತ್ಸೆ ಪತ್ತೆ ಮಾಡಬಹುದು, ಅಥವಾ LOLcats ಕ್ಲಿಕ್ಕಿಸಿ. ನಾನು ಕೇವಲ ಪ್ರವೇಶ ಪ್ರತಿ ಅರ್ಧ ನಿಮಿಷ ಮೀಸಲಿಡುವ ಬಗ್ಗೆ ತಪ್ಪಿತಸ್ಥ ಭಾವನೆ; ನನ್ನ ನಿರ್ಧಾರವನ್ನು 30 ತಮ್ಮ ಲ್ಯಾಂಡಿಂಗ್ ಪುಟ ಸ್ಕ್ಯಾನಿಂಗ್ ಸೆಕೆಂಡುಗಳು ಮತ್ತು ಬಹುಶಃ ಕೆಲವು ಅಸಂಬದ್ಧ ಕ್ಲಿಕ್ ಆಧರಿಸಿ ಮಾಡಿದರೆ ಅದು ಸ್ಪರ್ಧೆಯಲ್ಲಿ ನ್ಯಾಯ? ಆದರೆ ನಾನು ಒಂದು ಸಾಕ್ಷಾತ್ಕಾರ ಹೊಂದಿತ್ತು: ಹೌದು, 30 ಸೆಕೆಂಡುಗಳಲ್ಲಿ ನಿರ್ಧರಿಸುವ ಸಂಪೂರ್ಣವಾಗಿ ಸಂಪೂರ್ಣವಾಗಿ ಮೋಸದ, ಆದರೆ ಅದು ನಿಜವಾದ ಜಗತ್ತಿನಲ್ಲಿ ಕೆಲಸ ಮಾಡುತ್ತದೆ ಎಂಬುದನ್ನು ಸಹ ಇಲ್ಲಿದೆ. ನಿಮ್ಮ ವೆಬ್ ಸೈಟ್ ಮೂಲಕ ಕ್ಲಿಕ್ ಹೋಗುವ, ಬಹುಶಃ 30 ಸೆಕೆಂಡುಗಳಲ್ಲಿ ಇದನ್ನು ನೋಡಲು, ಮತ್ತು ಅದನ್ನು ಯೋಗ್ಯ ಎಂದು ನಿರ್ಧರಿಸಲು, ಅಥವಾ ತಮ್ಮ ಬ್ರೌಸರ್ ಮತ್ತು ದೋಷವನ್ನು ಔಟ್ ರಂದು ಆಲ್ಮೈಟಿ ಬ್ಯಾಕ್ ಬಟನ್ ಗೆ ತಲುಪುತ್ತದೆ. ಮೂವತ್ತು ಸೆಕೆಂಡುಗಳ ಸಹ ಸ್ವಲ್ಪ ಉದಾರ ಇರಬಹುದು. ಒಂದು ಕೆನಡಿಯನ್ ಅಧ್ಯಯನದಲ್ಲಿ, ಬಳಕೆದಾರರು ಎರಡನೇ ಅಡಿಯಲ್ಲಿ ವೆಬ್ಸೈಟ್ಗಳನ್ನು ಬಗ್ಗೆ ತಮ್ಮ ಮನಸ್ಸನ್ನು ಮಾಡಿದ.",
          languageName).SetName(languageName.ToString());
    }

    // ReSharper restore UnusedMethodReturnValue.Local
  }
}
