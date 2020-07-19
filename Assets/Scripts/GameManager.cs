using UnityEngine;
using System.Linq;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    #region Singelton
    public static GameManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one Instance of Inventory found");
            return;
        }

        instance = this;
    }
    #endregion

    public GameObject ReplyButton;
    public GameObject increaseScore;

    public Text followertext;
    public Text usernametext;
    public Text commenttext;
    public Text neededfollowerstext;
    public Text notifications;

    public int neededfollowers = 1000;

    public Color green;
    public Color red;
    public Color black;

    private char[] delimiterChars = { '.' };
    private static int followerscore = 0;

    #region All Arrays
    private string[] whitelist = { "4r5e", "5h1t", "5hit", "a55", "anal", "anus", "ar5e", "arrse", "arse", "ass", "ass-fucker", "asses", "assfucker", "assfukka", "asshole", "assholes", "asswhole", "a_s_s", "b!tch", "b00bs", "b17ch", "b1tch", "ballbag", "balls", "ballsack", "bastard", "beastial", "beastiality", "bellend", "bestial", "bestiality", "bi+ch", "biatch", "bitch", "bitcher", "bitchers", "bitches", "bitchin", "bitching", "bloody", "blow job", "blowjob", "blowjobs", "boiolas", "bollock", "bollok", "boner", "boob", "boobs", "booobs", "boooobs", "booooobs", "booooooobs", "breasts", "buceta", "bugger", "bum", "bunny fucker", "butt", "butthole", "buttmuch", "buttplug", "c0ck", "c0cksucker", "carpet muncher", "cawk", "chink", "cipa", "cl1t", "clit", "clitoris", "clits", "cnut", "cock", "cock-sucker", "cockface", "cockhead", "cockmunch", "cockmuncher", "cocks", "cocksuck ", "cocksucked ", "cocksucker", "cocksucking", "cocksucks ", "cocksuka", "cocksukka", "cok", "cokmuncher", "coksucka", "coon", "cox", "crap", "cum", "cummer", "cumming", "cums", "cumshot", "cunilingus", "cunillingus", "cunnilingus", "cunt", "cuntlick", "cuntlicker ", "cuntlicking ", "cunts", "cyalis", "cyberfuc", "cyberfuck ", "cyberfucked ", "cyberfucker", "cyberfuckers", "cyberfucking ", "d1ck", "damn", "dick", "dickhead", "dildo", "dildos", "dink", "dinks", "dirsa", "dlck", "dog-fucker", "doggin", "dogging", "donkeyribber", "doosh", "duche", "dyke", "ejaculate", "ejaculated", "ejaculates ", "ejaculating ", "ejaculatings", "ejaculation", "ejakulate", "f u c k", "f u c k e r", "f4nny", "fag", "fagging", "faggitt", "faggot", "faggs", "fagot", "fagots", "fags", "fanny", "fannyflaps", "fannyfucker", "fanyy", "fatass", "fcuk", "fcuker", "fcuking", "feck", "fecker", "felching", "fellate", "fellatio", "fingerfuck ", "fingerfucked ", "fingerfucker ", "fingerfuckers", "fingerfucking ", "fingerfucks ", "fistfuck", "fistfucked ", "fistfucker ", "fistfuckers ", "fistfucking ", "fistfuckings ", "fistfucks ", "flange", "fook", "fooker", "fuck", "fucka", "fucked", "fucker", "fuckers", "fuckhead", "fuckheads", "fuckin", "fucking", "fuckings", "fuckingshitmotherfucker", "fuckme ", "fucks", "fuckwhit", "fuckwit", "fudge packer", "fudgepacker", "fuk", "fuker", "fukker", "fukkin", "fuks", "fukwhit", "fukwit", "fux", "fux0r", "f_u_c_k", "gangbang", "gangbanged ", "gangbangs ", "gaylord", "gaysex", "goatse", "God", "god-dam", "god-damned", "goddamn", "goddamned", "hardcoresex ", "hell", "heshe", "hoar", "hoare", "hoer", "homo", "hore", "horniest", "horny", "hotsex", "jack-off ", "jackoff", "jap", "jerk-off ", "jism", "jiz ", "jizm ", "jizz", "kawk", "knob", "knobead", "knobed", "knobend", "knobhead", "knobjocky", "knobjokey", "kock", "kondum", "kondums", "kum", "kummer", "kumming", "kums", "kunilingus", "l3i+ch", "l3itch", "labia", "lmfao", "lust", "lusting", "m0f0", "m0fo", "m45terbate", "ma5terb8", "ma5terbate", "masochist", "master-bate", "masterb8", "masterbat*", "masterbat3", "masterbate", "masterbation", "masterbations", "masturbate", "mo-fo", "mof0", "mofo", "mothafuck", "mothafucka", "mothafuckas", "mothafuckaz", "mothafucked ", "mothafucker", "mothafuckers", "mothafuckin", "mothafucking ", "mothafuckings", "mothafucks", "mother fucker", "motherfuck", "motherfucked", "motherfucker", "motherfuckers", "motherfuckin", "motherfucking", "motherfuckings", "motherfuckka", "motherfucks", "muff", "mutha", "muthafecker", "muthafuckker", "muther", "mutherfucker", "n1gga", "n1gger", "nazi", "nigg3r", "nigg4h", "nigga", "niggah", "niggas", "niggaz", "nigger", "niggers", "nob", "nob jokey", "nobhead", "nobjocky", "nobjokey", "numbnuts", "nutsack", "orgasim", "orgasims", "orgasm", "orgasms", "p0rn", "pawn", "pecker", "penis", "penisfucker", "phonesex", "phuck", "phuk", "phuked", "phuking", "phukked", "phukking", "phuks", "phuq", "pigfucker", "pimpis", "piss", "pissed", "pisser", "pissers", "pisses", "pissflaps", "pissin", "pissing", "pissoff", "poop", "porn", "porno", "pornography", "pornos", "prick", "pricks", "pron", "pube", "pusse", "pussi", "pussies", "pussy", "pussys", "rectum", "retard", "rimjaw", "rimming", "shit", "s.o.b.", "sadist", "schlong", "screwing", "scroat", "scrote", "scrotum", "semen", "sex", "sh!+", "sh!t", "sh1t", "shag", "shagger", "shaggin", "shagging", "shemale", "shi+", "shit", "shitdick", "shite", "shited", "shitey", "shitfuck", "shitfull", "shithead", "shiting", "shitings", "shits", "shitted", "shitter", "shitters", "shitting", "shittings", "shitty", "skank", "slut", "sluts", "smegma", "smut", "snatch", "son-of-a-bitch", "spac", "spunk", "s_h_i_t", "t1tt1e5", "t1tties", "teets", "teez", "testical", "testicle", "tit", "titfuck", "tits", "titt", "tittie5", "tittiefucker", "titties", "tittyfuck", "tittywank", "titwank", "tosser", "turd", "tw4t", "twat", "twathead", "twatty", "twunt", "twunter", "v14gra", "v1gra", "vagina", "viagra", "vulva", "w00se", "wang", "wank", "wanker", "wanky", "whoar", "whore", "willies", "willy", "xrated", "xxx", "no u", "no you", "not", "neither", "unlike"};

    private string[] blacklist = { "ABUNDANT", "ADAPTABLE", "ADORABLE", "ADORED", "ADVENTUROUS", "AFFABLE", "AFFECTIONATE", "AGREEABLE", "ALLOWING", "ALTRUISTIC", "AMAZING", "AMBITIOUS", "AMIABLE", "AMICABLE", "AMUSING", "ANGELIC", "APPRECIATED", "APPRECIATIVE", "AUTHENTIC", "AWARE", "AWESOME", "BALANCED", "BEAUTIFUL", "BELOVED", "BEST", "BEYOND-FABULOUS", "BLESSED", "BLISSFUL", "BLITHESOME", "BOLD", "BRAVE", "BREATHTAKING", "BRIGHT", "BRILLIANT", "BROAD-MINDED", "CALM", "CAPABLE", "CAREFUL", "CARING", "CENTERED", "CHAMPION", "CHARISMATIC", "CHARMING", "CHEERFUL", "CHERISHED", "COMFORTABLE", "COMMUNICATIVE", "COMPASSIONATE", "CONFIDENT", "CONSCIENTIOUS", "CONSIDERATE", "CONTENT", "CONVIVIAL", "COURAGEOUS", "COURTEOUS", "CREATIVE", "CUTE", "DANDY", "DARING", "DAZZLED", "DECISIVE", "DEDICATED", "DELICIOUS", "DELIGHTFUL", "DESIRABLE", "DETERMINED", "DILIGENT", "DIPLOMATIC", "DISCREET", "DIVINE", "DYNAMIC", "EAGER", "EASYGOING", "EMOTIONAL", "EMPATHIC", "EMPATHETIC", "EMPOWERED", "ENCHANTED", "ENDLESS", "ENERGETIC", "ENERGIZED", "ENLIGHTENED", "ENLIVENED", "ENOUGH", "ENTHUSIASTIC", "ETERNAL", "EXCELLENT", "EXCITED", "EXHILARATED", "EXPANDED", "EXPANSIVE", "EXQUISITE", "EXTRAORDINARY", "EXUBERANT", "FABULOUS", "FAIR-MINDED", "FAITHFUL", "FANTASTIC", "FAVORABLE", "FEARLESS", "FLOURISHED", "FLOWING", "FOCUSED", "FORCEFUL", "FORGIVING", "FORTUITOUS", "FRANK", "FREE", "FREE-SPIRITED", "FRIENDLY", "FULFILLED", "FUN", "FUN-LOVING", "FUNNY", "GENEROUS", "GENIAL", "GENIUS", "GENTLE", "GENUINE", "GIVING", "GLAD", "GLORIOUS", "GLOWING", "GODDESS", "GOOD", "GOOD HEALTH", "GOODNESS", "GRACEFUL", "GRACIOUS", "GRATEFUL", "GREAT", "GREGARIOUS", "GROUNDED", "HAPPY", "HAPPY-HEARTED", "HARD-WORKING", "HARMONIOUS", "HEALTHY", "HEARTFULL", "HEARTWARMING", "HEAVEN", "HELPFUL", "HIGH-SPIRITED", "HOLY", "HONEST", "HOPEFUL", "HUMOROUS", "ILLUMINATED", "IMAGINATIVE", "IMPARTIAL", "INCOMPARABLE", "INCREDIBLE", "INDEPENDENT", "INEFFABLE", "INNOVATIVE", "INSPIRATIONAL", "INSPIRED", "INTELLECTUAL", "INTELLIGENT", "INTUITIVE", "INVENTIVE", "INVIGORATED", "INVOLVED", "IRRESISTIBLE", "JAZZED", "JOLLY", "JOVIAL", "JOYFUL", "JOYOUS", "JUBILANT", "JUICY", "JUST", "JUVENESCENT", "KALON", "KIND", "KIND-HEARTED", "KISSABLE", "KNOWINGLY", "KNOWLEDGEABLE", "LIVELY", "LOVABLE", "LOVED", "LOVELY", "LOVING", "LOYAL", "LUCKY", "LUXURIOUS", "MAGICAL", "MAGNIFICENT", "MARVELOUS", "MEMORABLE", "MIND-BLOWING", "MINDFUL", "MIRACLE", "MIRACULOUS", "MIRTHFUL", "MODEST", "NEAT", "NICE", "NIRVANA", "NOBLE", "NON-RESISTANT", "NOURISHED", "NURTURED", "OPEN", "OPEN-HEARTED", "OPEN-MINDED", "OPTIMISTIC", "OPULENT", "ORIGINAL", "OUTSTANDING", "OWNING-MY-POWER", "PASSIONATE", "PATIENT", "PEACEFUL", "PERFECT", "PERSISTENT", "PHILOSOPHICAL", "PIONEERING", "PLACID", "PLAYFUL", "PLUCKY", "POLITE", "POSITIVE", "POWERFUL", "PRACTICAL", "PRECIOUS", "PRO-ACTIVE", "PROPITIOUS", "PROSPEROUS", "QUICK-WITTED", "QUIET", "RADIANT", "RATIONAL", "READY", "RECEPTIVE", "REFRESHED", "REJUVENATED", "RELAXED", "RELIABLE", "RELIEVED", "REMARKABLE", "RENEWED", "RESERVED", "RESILIENT", "RESOURCEFUL", "RICH", "ROMANTIC", "SACRED", "SAFE", "SATISFIED", "SECURED", "SELF-ACCEPTING", "SELF-CONFIDENT", "SELF-DISCIPLINED", "SELF-LOVING", "SENSATIONAL", "SENSIBLE", "SENSITIVE", "SERENE", "SHINING", "SHY", "SINCERE", "SMART", "SOCIABLE", "SOULFUL", "SPECTACULAR", "SPLENDID", "STELLAR", "STRAIGHTFORWARD", "STRONG", "STUPENDOUS", "SUCCESSFUL", "SUPER", "SUSTAINED", "SYMPATHETIC", "THANKFUL", "THOUGHTFUL", "THRILLED", "THRIVING", "TIDY", "TOUGH", "TRANQUIL", "TRIUMPHANT", "TRUSTING", "ULTIMATE", "UNASSUMING", "UNBELIEVABLE", "UNDERSTANDING", "UNF**KWITABLE", "UNIQUE", "UNLIMITED", "UNREAL", "UPLIFTED", "VALUABLE", "VERSATILE", "VIBRANT", "VICTORIOUS", "VIVACIOUS", "WARM", "WARMHEARTED", "WEALTHY", "WELCOMED", "WHOLE", "WHOLEHEARTEDLY", "WILLING", "WISE", "WITTY", "WONDERFUL", "WONDROUS", "WORTHY", "XOXO", "YOUNG-AT-HEART", "YOUTHFUL", "YUMMY", "ZAPPY", "ZESTFUL", "ZING"};

    private string[] comments = { "If laughter is the best medicine, your face must be curing the world.", "You're so ugly, you scared the crap out of the toilet.", "Your family tree must be a cactus because everybody on it is a prick.", "No I'm not insulting you, I'm describing you.", "It's better to let someone think you are an Idiot than to open your mouth and prove it.", "If I had a face like yours, I'd sue my parents.", "Your birth certificate is an apology letter from the condom factory.", "I guess you prove that even god makes mistakes sometimes.", "The only way you'll ever get laid is if you crawl up a chicken's ass and wait.", "You're so fake, Barbie is jealous.", "I’m jealous of people that don’t know you!", "My psychiatrist told me I was crazy and I said I want a second opinion. He said okay, you're ugly too.", "You're so ugly, when your mom dropped you off at school she got a fine for littering.", "If I wanted to kill myself I'd climb your ego and jump to your IQ.", "You must have been born on a highway because that's where most accidents happen.", "Brains aren't everything.", "In your case they're nothing.", "I don't know what makes you so stupid, but it really works.", "I can explain it to you, but I can’t understand it for you.", "Roses are red violets are blue, God made me pretty, what happened to you?", "Behind every fat woman there is a beautiful woman. No seriously, your in the way.", "Calling you an idiot would be an insult to all the stupid people.", "You, sir, are an oxygen thief!", "Some babies were dropped on their heads but you were clearly thrown at a wall.", "Why don't you go play in traffic.", "Please shut your mouth when you’re talking to me.", "I'd slap you, but that would be animal abuse.", "They say opposites attract. I hope you meet someone who is good-looking, intelligent, and cultured.", "Stop trying to be a smart ass, you're just an ass.", "The last time I saw something like you, I flushed it.", "I'm busy now. Can I ignore you some other time?", "You have Diarrhea of the mouth; constipation of the ideas.", "If ugly were a crime, you'd get a life sentence.", "Your mind is on vacation but your mouth is working overtime.", "I can lose weight, but you’ll always be ugly.", "Why don't you slip into something more comfortable... like a coma.", "Shock me, say something intelligent.", "If your gonna be two faced, honey at least make one of them pretty.", "Keep rolling your eyes, perhaps you'll find a brain back there.", "You are not as bad as people say, you are much, much worse.", "Don't like my sarcasm, well I don't like your stupid.", "I don't know what your problem is, but I'll bet it's hard to pronounce.", "You get ten times more girls than me? Ten times zero is zero...", "There is no vaccine against stupidity.", "You're the reason the gene pool needs a lifeguard.", "Sure, I've seen people like you before - but I had to pay an admission.", "How old are you? - Wait I shouldn't ask, you can't count that high.", "Have you been shopping lately? They're selling lives, you should go get one.", "You're like Monday mornings, nobody likes you.", "Of course I talk like an idiot, how else would you understand me?", "All day I thought of you... I was at the zoo.", "To make you laugh on Saturday, I need to you joke on Wednesday.", "You're so fat, you could sell shade.", "I'd like to see things from your point of view but I can't seem to get my head that far up my ass.", "Don't you need a license to be that ugly?", "My friend thinks he is smart. He told me an onion is the only food that makes you cry, so I threw a coconut at his face.", "Your house is so dirty you have to wipe your feet before you go outside.", "If you really spoke your mind, you'd be speechless.", "Stupidity is not a crime so you are free to go.", "You are so old, when you were a kid rainbows were black and white.", "If I told you that I have a piece of dirt in my eye, would you move?", "You so dumb, you think Cheerios are doughnut seeds.", "So, a thought crossed your mind? Must have been a long and lonely journey.", "You are so old, your birth-certificate expired.", "Every time I'm next to you, I get a fierce desire to be alone.", "You're so dumb that you got hit by a parked car.", "Keep talking, someday you'll say something intelligent!", "You're so fat, you leave footprints in concrete.", "How did you get here? Did someone leave your cage open?", "Pardon me, but you've obviously mistaken me for someone who gives a damn.", "Wipe your mouth, there's still a tiny bit of bullshit around your lips.", "Don't you have a terribly empty feeling - in your skull?", "As an outsider, what do you think of the human race?", "Just because you have one doesn't mean you have to act like one.", "We can always tell when you are lying. Your lips move.", "Are you always this stupid or is today a special occasion?"};

    private string[] username = { "abana", "Friendette", "corena", "TheWell-to-doGamer", "Ferdynefx", "eliseev", "ellis", "kellie", "NewyorkMedic70", "sandstonephalange", "leftwich", "tessy", "isisiskenderun", "beastings", "elliott", "GooblectreKatiebPaint85", "whitmer", "coolHapless", "StuffyKatieb38", "TvTyler", "manvell", "protestation", "AAmusinghaha", "tntwobble", "BalanceGrape", "gounod", "tangential", "increscent", "MinivanSilly14", "daniil", "Sion Apave", "ratcliff", "CoolInterest19", "cyrill", "Kuna Order", "wakeen", "misdemeanor", "FearKingDogg", "MidTexans80", "srinagar", "Football", "Junkyliance", "AaronVacation63", "thingumabob", "Ill-fatedTweets", "NewyorkWalrus27", "LuvVintageRosa", "10Dancer", "brief", "AWorried10", "nataline", "bentley", "coolLethal", "portend", "fireboat", "longanimity", "edwin", "HaleyNumber", "perfectOld10", "kristine", "truncheon", "escheat", "AConsciousgame", "Ryany Rivis", "sahaptin", "woodbridge", "julio", "portwine", "moreville", "duisburg", "Vidarr Minne", "NewsCoolest57", "CampyIan", "TheSkinnyGamer", "perfectHeavy10", "admin", "alenk", "niello", "metagnathous", "skirr", "HardNewyork64", "fimbriation", "parette", "norinenorita", "edroi", "schizogony", "coolCool", "recliner", "Pippaud", "malvinamalvino", "whitherward", "brogan", "ForksSilly49", "linen", "elena", "ChicagoGovernment48", "SpearNewyork41", "alexe", "PantrySilly17", "Aquaticks", "coolHandsomely", "grace", "firebamboo", "jeremiah", "lieberman", "SmokyChicago", "bloodhound", "coolAdhoc", "coolFamiliar", "leges", "SparkFun", "funnyRitzy", "TheHandyGamer", "belov", "funnyUnsuitable", "TheWorkableGamer", "AGeneralHaha", "testy", "stockwell", "ashbaugh", "ANaughtyhaha", "AThankfulgame", "CoolForm91", "SaltyLa33", "lamellar", "FootballUsernames", "gosser", "Funfage", "bollard", "PaleManatee", "smoothspoken", "eelgrass", "kimura", "gable", "Joshua Colley", "aragats", "FallFootball", "truditrudie", "PinkCoolest8", "dainedainty", "coolSerious", "Randy Jackson", "Petodd Sterson", "FriendsHaley", "streamlined", "Schoollami", "attempt", "AHulkingpoet", "Lockroach", "kaftan", "aksenov", "gland", "school", "vibrant", "billing", "mcvay", "mineral", "vinia", "lynnalynne", "funnyBent", "Daro Zapal", "borisov", "clinkscales", "delija", "Football10", "morisco", "phenobarbitone", "director", "burbank", "gravelhippo", "consul", "NightSilly43", "paraesthesia", "GreyTyler", "Funnymyce", "afanasiy", "dumas", "shwalb", "bartlet", "HeadyNewyork54", "ACloudy10", "DolphinTrevor71", "dearing", "Morcedecu", "alethiaaletta", "SweaterCoolest94", "ABigpoet", "DavisAberrant10", "ChicagoSail", "hartsfield", "applefartlek", "Edm10", "design", "AMoaningpoet", "dmitri", "karena", "amongst", "morris", "phosphate", "nessim", "birchwoodpoppycock", "EdmSupreme", "Anier Bertson", "venose", "ladon", "coolDepressed", "coolAware", "AObtainable10", "falconer", "seduction", "coolMaterial", "DireCool15", "TheRebelGamer", "gowan", "TheMarvelousGamer", "bobrov", "business", "coffeng", "pansy", "SpiderHaley", "CakeKatieb67", "AJazzy10", "eipper", "ChicagoBushes65", "alexeev", "roundhouse", "TrevorIguana89", "harrietteharrigan", "takashi", "hankering", "kesley", "advert", "10PiraUsernames", "dinner", "shondrashone", "DavisHandsome10", "CoolestSkunk98", "TheFrightenedGamer", "coolFull", "illuminate", "AHigh-pitchedHaha", "consult", "FunScrew", "PerverseNewyork0", "NewyorkDad13", "leverpeeve", "declivous", "Commentca", "habitue", "miltonmilty", "Jackeith Welley", "midweek", "easterly", "ManiakChikk", "fritter", "contact", "boris", "adver", "perfectUgliest10", "BuddieChone", "funnyNauseating", "annabelle", "diphtheria", "necaise", "Deep10", "coolDevilish", "dirtquean", "tetratomic", "cochran", "SteepCool38", "EdmBrilliant", "bivins", "AAbsurd10", "Postiliaen", "irmgardirmina", "coolCareless", "clathrate", "TheUnbecomingGamer", "Kirkman", "cabbagehead", "fawnia", "funnyBad", "litigate", "Garm Hamne", "daemon", "LaGame65", "ADeadpanHaha", "ClumsyCool22", "equivalence", "andreev", "melanochroi", "localism", "carrasquillo", "Blebelco", "corral", "Horo Typhe", "byelaw", "SillyMicrophone90", "Amineshi", "TaxAaron9", "DustyLa29", "BumblebeeCool24", "belousov", "twister", "ALittlegame", "abdominal", "harsho", "coolGlorioushaha", "steffaniesteffen", "funnyBored", "fayfayal", "smooth", "averell", "dorofeev", "DeadCoolest99", "denti", "indogermanic", "aircougar", "dmitry", "SplitCoolest32", "beall", "FootballCall", "LaGrandmother61", "TreatmentChicago", "AMadlyhaha", "Cheludin", "designer", "ASharphaha", "Bauda", "aleksey", "piled", "laughingstock", "anthon", "andreandrea", "misjudge", "catarina", "jalapa", "CubCool3", "ThePrivateGamer", "Garm Baize", "WitchTexans54", "arseniy", "closed", "arkadiy", "RecklessClam", "standice", "watchcase", "SillyPayment28", "Polyptorm", "sulemasulf", "manager", "schumann", "Edmunaudi", "AQuickest10", "tirrell", "pitch", "bathhouse", "CrowTrevor53", "screed", "DavisMean10", "Mycri", "swill", "betel", "SillyOil85", "anatol", "AValuablehaha", "eggpoppysmic", "killingsworth", "beluga", "cripps", "KatiebCork92", "sugarcaneduty", "HaleyPillowcase", "PartyFun", "separate", "alenka", "TexansMilkyway56", "campbell", "Edm10Football", "amherst", "ATelling10", "BrakeCoolest68", "proudman", "TheShyGamer", "oscular", "cribbage", "antithesis", "fernandes", "Geckoco", "heartbreak", "UkuleleKatieb37", "agafonov", "examination", "blohin", "percent", "jewess", "lennie", "sneer", "cruel", "overcompensation", "trapdoormumpsimus", "debroahdebs", "DrearyLa41", "CoolestPonytail93", "10Football", "coolOptimal", "TheGorgeousGamer", "sardinian", "TheMagnificentGamer", "flintglabella", "account", "Upalc", "burnham", "designe", "lahdidah", "photoengrave", "impersonal", "Broadwayon", "10ContentFunny", "shreve", "kassiekassity", "TrevorOrnament98", "coolHelpless", "EmptyTrevor17", "KatiebOwner47", "spondaic", "grivation", "coolOmniscient", "KatiebTimber48", "wordsmith", "sales", "ploch", "Incenau", "roswald", "ASuccinctgame", "coolVictorious", "kortneykoruna", "gilbertine", "FunnyBomber", "northwestward", "andrew", "bonemealchocks", "oneiric", "adventitia", "montserrat", "cheston", "Funmogr", "TameCoolest53", "elemental", "agnosia", "marquis", "herbertherbicide", "acroter", "FarmerTexans59", "Meratium", "Scara", "knudson", "RiceKatieb47", "SickCool31", "andre", "AHungry10", "MaidTrevor58", "postiche", "shire", "kenti", "argenteuil", "aleksander", "ScarfSilly33", "andrey", "VanAaron39", "lopsided", "GpsKatieb62", "ultann", "iluminadailwain", "iives", "CoolOiltanker33", "ChicagoDisgust4", "SnugFun", "substantial", "AActuallyhaha", "TheCuteGamer", "ATypicalgame", "schilt", "DimStork", "Chrouple", "tuberculous", "entrant", "aureneebirrell", "Sanderonat", "coolMushy", "incurable", "PeacefulTrevor45", "SandSilly76", "profit", "mapfolderol", "arhipov", "menorrhagia", "alena", "shonda", "cajeput", "AScreechingpoet", "gastroscope", "dillon", "leatherdiphthong", "billin", "Frence Turnez", "ColossalChicago92", "Gavin Javik", "ANineHaha", "FootballEdm", "BlackTrevor44", "pitcher", "malaysia", "limonene", "hialeah", "dordogne", "afanasev", "anisimov", "kiowa", "Jamai", "TightChicago", "Chinaut", "McMohawkMessage", "sewer", "BaldCoolest55", "SoreCoolest83", "surinam", "arrestment", "NewyorkPolice51", "deicer", "blasphemy", "hannan", "bookshelfsousaphone", "vermicelli", "signpostcahoots", "FruitSilly62", "rosefish", "oppress", "AThirdpoet", "manta", "WorthyChicago4", "coolCrowded", "papert", "FunHeat", "alekse", "Rista", "lightner", "Funnymo", "Peakellent", "Amighost", "TheLastGamer", "egorov", "WatermillHaley", "TheAgreeableGamer", "perfectAbsent10", "AuthorJrCutie", "aleksandr", "trinitrophenol", "company", "coolSlow", "abramov", "vendas", "AWaggishgame", "dmitriy", "TheDisastrousGamer", "orson", "bakemeier", "contato", "WasabiFun", "frobisher", "PlutoCool7", "FootballFootway", "TheSourGamer", "AGrumpygame", "reynolds", "coaloreglom", "EdmFunUsernames", "killigrew", "NewyorkScrew96", "merrymaking", "tricrotic", "TheZestyGamer", "TaserSilly19", "valoniah", "HaleyStranger", "CynicalChicago12", "funnyDecorous", "TheHeadyGamer", "FallTyler", "echinus", "brittney", "Ostritches", "doubly", "BareCool79", "HorribleSailor", "fomichev", "caroche", "torry", "mazer", "Porro Eren", "tritheism", "dillydally", "artemiy", "funderburk", "emeraldgobsmacked", "Vinor Donnall", "LiquidTyler", "AaronYarn87", "antimasque", "aleksandrov", "Rewayn Morry", "10Time", "pepys", "spokeswoman", "RepulsiveHaley", "multiplier", "whacking", "TylerCellar", "Jeffry Morgonz", "avone.w.corre", "SilkyNewyork82", "carman", "phaidra", "constantine", "TheGreyGamer", "canaliculus", "stallion", "highlight", "roustabout", "avdeev", "thacker", "aires", "pharsalus", "optional", "eskew", "bordeaux", "melonocciput", "consummate", "chastitychasuble", "SalamiChicago", "snack", "blinov", "nicolasanicolau", "hakenkreuz", "TexansMoney90", "kallman", "ASelfish10", "bilabiate", "BathroomCoolest58", "GreenSilly83", "pokelogan", "delighted", "teatime", "subsequent", "albeit", "usurious", "borya", "burov", "NewyorkBeast10", "japha", "FootballNozy", "robbirobbia", "soutane", "TreatmentTrevor86", "ACowardlyhaha", "torchgewgaw", "relator", "choate", "sulphurfeline", "LinkinElite", "navel", "pyknic", "DeepFun", "scurry", "loehr", "hypno", "exalted", "WarGod", "checker", "perfectThoughtful10", "accusatorial", "largo", "flagon", "ALanguidhaha", "ormazd", "boulware", "latimer", "FrigateTexans84", "EdmTrue", "freeland", "alexei", "AMighty10", "EdmFun", "LikelyTyler", "alexey", "biryukov", "10Sport", "Edmunni", "BatmanTexans28", "Cottin Patte", "saltant", "CakeUsernames", "snowballpluck", "lilypadgoggles", "AHushedpoet", "DavisGentle10", "intramural", "alphard", "dawkins", "gasper", "mcginn", "support", "baranov", "contactus" };

    #endregion

    private void Start()
    {
        followerscore = PlayerPrefs.GetInt("HighScore");
        followertext.text = PlayerPrefs.GetInt("HighScore", followerscore).ToString();
        neededfollowerstext.text = neededfollowers.ToString();
        NextComment();
    }

    private void SetHighScore()
    {
        if (followerscore < 0)
        {
            PlayerPrefs.SetInt("HighScore", 0);
            return;
        }

        PlayerPrefs.SetInt("HighScore", followerscore);
    }

    public void CalculateScore(string sentence)
    {
        followerscore -= CheckBlackList(sentence);
        followerscore += (CheckWhiteList(sentence) * 2);
        followerscore += CheckSameasComment(sentence);

        string[] str = sentence.Split(delimiterChars);

        int str_size = str.Length;

        int i = 0;

        for (i = 0; i < str_size; i++)
        {
            if (checkSentence(str[i].ToCharArray()))
                followerscore++;
            else
                followerscore--;
        }

        int difference = followerscore - Convert.ToInt32(followertext.text);

        GameObject increasescore = Instantiate(increaseScore) as GameObject;
        increasescore.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
        Destroy(increasescore, 2f);

        Text increaseScoretext = increasescore.GetComponent<Text>() as Text;
        increaseScoretext.text = difference.ToString();

        if (difference > 0)
        {
            FindObjectOfType<AudioManager>().Play("IncreaseScore");
            increaseScoretext.color = green;
            increaseScoretext.text = "+" + difference.ToString();
        }

        else if (difference == 0)
        {
            increaseScoretext.color = black;
        }

        else if (difference < 0)
        {
            FindObjectOfType<AudioManager>().Play("DecreaseScore");
            increaseScoretext.color = red;
        }

        followertext.text = followerscore.ToString();
        SetHighScore();

        if (followerscore <= 0)
            EndGame();

        if (followerscore >= neededfollowers)
            GameWon();

        NextComment();
    }

    // Method to check a given sentence for given rules 
    private bool checkSentence(char[] str)
    {
        // Calculate the length of the string. 
        int len = str.Length;

        if (str == null || len <= 0)
            return false;

        // Check that the first character lies in [A-Z]. 
        // Otherwise return false. 
        if (str[0] < 'A' || str[0] > 'Z')
            return false;

        // If the last character is not a full stop(.)  
        // no need to check further. 
        if (str[len - 1] != '.')
            return false;

        // Maintain 2 states. Previous and current state  
        // based on which vertex state you are.  
        // Initialise both with 0 = start state. 
        int prev_state = 0, curr_state = 0;

        // Keep the index to the next character in the string. 
        int index = 1;

        // Loop to go over the string. 
        while (index <= str.Length)
        {

            // Set states according to the input characters  
            // in the string and the rule defined in the description. 
            // If current character is [A-Z]. Set current state as 0. 
            if (str[index] >= 'A' && str[index] <= 'Z')
                curr_state = 0;

            // If current character is a space.  
            // Set current state as 1. 
            else if (str[index] == ' ')
                curr_state = 1;

            // If current character is [a-z].  
            // Set current state as 2. 
            else if (str[index] >= 'a' && str[index] <= 'z')
                curr_state = 2;

            // If current state is a dot(.). 
            // Set current state as 3. 
            else if (str[index] == '.')
                curr_state = 3;

            // Validates all current state with previous state  
            // for the rules in the description of the problem. 
            if (prev_state == curr_state && curr_state != 2)
                return false;

            if (prev_state == 2 && curr_state == 0)
                return false;

            // If we have reached last state and previous state  
            // is not 1, then check next character. If next character  
            // is '\0', then return true, else false 
            if (curr_state == 3 && prev_state != 1)
                return (index + 1 == str.Length);

            index++;

            // Set previous state as current state  
            // before going over to the next character. 
            prev_state = curr_state;
        }
        return false;
    }

    private void EndGame()
    {
        SceneManager.LoadScene("GameOver");
    }

    private void GameWon()
    {
        SceneManager.LoadScene("GameWon");
    }

    private string RemoveWhitespace(string input)
    {
        return new string(input.ToCharArray()
            .Where(c => !Char.IsWhiteSpace(c))
            .ToArray());
    }

    private void NextComment()
    {
        commenttext.text = comments[UnityEngine.Random.Range(0, comments.Length - 1)];
        usernametext.text = username[UnityEngine.Random.Range(0, username.Length - 1)];
        ReplyButton.SetActive(true);
    }

    private int CheckWhiteList(string Wort)
    {
        int scorechange = 0;

        foreach (var item in whitelist)
        {
            if (RemoveWhitespace(Wort.ToLower()).Contains(item.ToLower()))
            {
                scorechange++;
            }
        }

        return scorechange;
    }

    private int CheckBlackList(string Wort)
    {
        int scorechange = 0;

        foreach (var item in blacklist)
        {
            if (RemoveWhitespace(Wort.ToLower()).Contains(item.ToLower()))
            {
                scorechange++;
            }
        }

        return scorechange;
    }

    private int CheckSameasComment(string Wort)
    {
        int scorechange = 0;

        foreach (var item in commenttext.text.Split(null))
        {
            if (RemoveWhitespace(Wort.ToLower()).Contains(item.ToLower()))
            {
                scorechange++;
            }
        }

        return scorechange;
    }

    public void BackToMenu()
    {
        FindObjectOfType<AudioManager>().Play("Select");
        SceneManager.LoadScene("Menu");
    }
}
