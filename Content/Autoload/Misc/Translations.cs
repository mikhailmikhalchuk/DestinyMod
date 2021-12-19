using TheDestinyMod.Core.Autoloading;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Terraria.Localization;

namespace TheDestinyMod.Content.Autoloading.Misc
{
	public class Translations : IAutoloadable
	{
		public void IAutoloadable_Load(IAutoloadable createdObject) // :fear:
        {
            if (Main.dedServ)
            {
                return;
            }

            TheDestinyMod mod = TheDestinyMod.Instance;

            int taxCollector = NPC.FindFirstNPC(NPCID.TaxCollector);

            ModTranslation text = mod.CreateTranslation("AgentOfNine1");
            text.SetDefault("I may be here when you return.");
            text.AddTranslation(GameCulture.Spanish, "Talvez esté aqui cuando vuelvas.");
            text.AddTranslation(GameCulture.Polish, "Będę tutaj jak powrócisz.");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("AgentOfNine2");
            text.SetDefault("These are from the Nine.");
            text.AddTranslation(GameCulture.Spanish, "Esto es de los Nueve.");
            text.AddTranslation(GameCulture.Polish, "Oni są z tej dziewiątki.");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("AgentOfNine3");
            text.SetDefault("My will is not my own. Is yours?");
            text.AddTranslation(GameCulture.Spanish, "Mi voluntad no es mia. ¿Y la tuya?");
            text.AddTranslation(GameCulture.Polish, "Moja wola nie jest moją własną. A twoja?");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("AgentOfNine4");
            text.SetDefault("The Traveler's song echoes on.");
            text.AddTranslation(GameCulture.Spanish, "La canción del Viajero resuena.");
            text.AddTranslation(GameCulture.Polish, "Pieśń Wędrowcy rozbrzmiewa echem.");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("AgentOfNine5");
            text.SetDefault("I bring a message from the Nine.");
            text.AddTranslation(GameCulture.Spanish, "Traigo un mensaje de los Nueve.");
            text.AddTranslation(GameCulture.Polish, "Przybywam z wiadomością od dziewiątki.");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("AgentOfNine6");
            text.SetDefault("Do not be alarmed. I know no reason to cause you harm.");
            text.AddTranslation(GameCulture.Spanish, "No te asustes. No conozco ninguna razón para causarte daño.");
            text.AddTranslation(GameCulture.Polish, "Nie przejmuj się. Nie mam powodów żeby cię skrzywdzić.");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("AgentOfNine7");
            text.SetDefault("To do what you say, is to speak in a language of pure meaning.");
            text.AddTranslation(GameCulture.Spanish, "Hacer lo que dices, es hablar un idioma de puro significado.");
            text.AddTranslation(GameCulture.Polish, "Robienie tego co mówisz to mówienie w języku o czystym znaczeniu.");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("AgentOfNine8");
            text.SetDefault("Do not go looking for the Nine. They will come to you.");
            text.AddTranslation(GameCulture.Spanish, "No busques a los Nueve. Ellos vendran a ti.");
            text.AddTranslation(GameCulture.Polish, "Nie szukaj dziewiątki. To ona znajdzie ciebie.");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("AgentOfNine9");
            text.SetDefault("You must stop eating salted popcorn.");
            text.AddTranslation(GameCulture.Spanish, "Deberias dejar de comer palomitas de maíz saladas.");
            text.AddTranslation(GameCulture.Polish, "Musisz przestać jeść ten słony popcorn.");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("AgentOfNine10");
            text.SetDefault("I am here for a reason, I just...cannot remember it.");
            text.AddTranslation(GameCulture.Spanish, "Estoy aqui por una razón, pero yo... no puedo recordarla.");
            text.AddTranslation(GameCulture.Polish, "Jestem tutaj z powodu tylko nie pamiętam jakiego.");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("AgentOfNine11");
            text.SetDefault("So much Light here, I suppose I feel...pain.");
            text.AddTranslation(GameCulture.Spanish, "Tanta Luz aquí, supongo que siento... dolor.");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("Cryptarch1");
            text.SetDefault("A party, you say? I'm much too busy decrypting these Vex artifacts, thank you.");
            text.AddTranslation(GameCulture.Polish, "Impreza powiadasz? Jestem bardziej zajęty deszyfrowaniem tych artefaktów od Vexów, dziękuje.");
            text.AddTranslation(GameCulture.Spanish, "¿Una escuadra, dices? Estoy muy ocupado desencriptando estos artefactos Vex, gracias.");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("Cryptarch2");
            text.SetDefault("Ah! This lack of light makes it hard to do anything around here.");
            text.AddTranslation(GameCulture.Polish, "Przez brak światła sprawia, że trudno tu co kolwiek zrobić.");
            text.AddTranslation(GameCulture.Spanish, "¡Ah! la falta de luz hace mas dificil hacer cualquier cosa por aquí.");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("Cryptarch3");
            text.SetDefault("Vex encryption. Unbreakable? Ha, so they say.");
            text.AddTranslation(GameCulture.Polish, "Szyfr Vexów nie do złamania? Ha tylko tak mówią.");
            text.AddTranslation(GameCulture.Spanish, "Encriptación vex. ¿Irrompible? Ja, eso dicen.");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("Cryptarch4");
            text.SetDefault("What have you got for me, Guardian?");
            text.AddTranslation(GameCulture.Polish, "Co masz dla mnie, Strażniku?");
            text.AddTranslation(GameCulture.Spanish, "¿Que me trajiste, Guardián?");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("Cryptarch5");
            text.SetDefault("Rasputin's fingerprints are all over this data. He doesn't even care if we know.");
            text.AddTranslation(GameCulture.Polish, "Odciski Rasputina są wszędzie. Go nie obchodzi że my wiemy.");
            text.AddTranslation(GameCulture.Spanish, "Las huellas de Rasputin estan sobre todos estos datos. Ni siquiera le importa si nosotros lo sabemos.");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("Cryptarch6");
            text.SetDefault("What challenges have you brought today, Guardian?");
            text.AddTranslation(GameCulture.Polish, "Jakie wyzwanie przyniosłeś dzisiaj, Strażniku?");
            text.AddTranslation(GameCulture.Spanish, "¿Que desafio me trajiste hoy, Guardián?");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("Cryptarch7");
            text.SetDefault("These are forgeries. Someone is wasting our time!");
            text.AddTranslation(GameCulture.Polish, "To są podróbki. Ktoś marnuje nasz czas!");
            text.AddTranslation(GameCulture.Spanish, "Estas son falsificaciones. ¡Alguien nos está haciendo perder el tiempo!");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("Cryptarch8");
            text.SetDefault("Hmm, this one is labeled \"Bigm55\"...yes, Guardian?");
            text.AddTranslation(GameCulture.Spanish, "Hmm, esta está etiquetada como \"Bigm55\"... ¿Si, Guardián?");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("RelicShard");
            text.SetDefault("Relic Shards have begun to grow!");
            text.AddTranslation(GameCulture.Polish, "Odłamki reliktów zaczęły rosnąć!");
            text.AddTranslation(GameCulture.Spanish, "¡Fragmentos de Reliquia empezaron a aparecer!");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("Zavala1");
            text.SetDefault("Guardian! This is your chance to take down this profound threat, do not get distracted!");
            text.AddTranslation(GameCulture.Polish, "Strażniku! to jest twoja szansa na pokonanie tego gruntownego zagrożenia, nie rozpraszaj się!");
            text.AddTranslation(GameCulture.Spanish, "¡Guardián! ¡Esta es tu oportunidad para acabar con esta gran amenaza, no te distraigas!");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("Zavala2");
            text.SetDefault("Get back out there, Guardian, and eliminate this threat!");
            text.AddTranslation(GameCulture.Polish, "Wracaj tam strażniku i pozbądź się tego zagrożenia!");
            text.AddTranslation(GameCulture.Spanish, "¡Sal afuera, Guardián, y elimina esta amenaza!");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("Zavala3");
            text.SetDefault("Guardian, you can do this. I believe in you, as does the rest of the Vanguard.");
            text.AddTranslation(GameCulture.Polish, "Uda ci się strażniku, wierzę w ciebie tak samo jak cała reszta Straży przedniej.");
            text.AddTranslation(GameCulture.Spanish, "Guardián, puedes hacer esto. Creo en ti, como el resto de la Vanguardia.");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("Zavala4");
            text.SetDefault("Guardian, you've slain some of the worst enemies the City and the Vanguard have ever seen, and for that, I thank you.");
            text.AddTranslation(GameCulture.Polish, "Strażniku, zabiłeś jednych z najgorszych wrogów jakich Miasto i Straż Przednia kiedykolwiek widziały i za to, Dziękuje tobie.");
            text.AddTranslation(GameCulture.Spanish, "Guardián, eliminaste a unos de los peores enemigos que la Ciudad y la Vanguardia hayan visto, y por eso, te agradezco.");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("Zavala5");
            text.SetDefault("Guardian, it's been brought to my attention that you may be partaking in...unsolicited activities. As long as it's for the good of the City.");
            text.AddTranslation(GameCulture.Polish, "Strażniku, zwrócono mi uwagę że możesz brać udział w niedozwolonych zajęć. O ile to dla dobra miasta.");
            text.AddTranslation(GameCulture.Spanish, "Guardián, me llamó la atencion que tal vez estes completando... actividades no solicitadas. Mientras que sea por el bien de la Ciudad.");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("Zavala6");
            text.SetDefault("I didn't authorize any party...but I guess we can take advantage of the moment while it lasts.");
            text.AddTranslation(GameCulture.Polish, "Nie autoryzowałem żadnej imprezy… ale myślę, że możemy wykorzystać ten moment, dopóki trwa.");
            text.AddTranslation(GameCulture.Spanish, "No autorizé ninguna escuadra... pero supongo que podriamos tomar provecho del momento mientras dura.");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("Zavala7");
            text.SetDefault("The Darkness is extremely strong here, Guardian.");
            text.AddTranslation(GameCulture.Polish, "Ciemność jest tutaj wysoce silna Strażniku.");
            text.AddTranslation(GameCulture.Spanish, "La Oscuridad es demasiado fuerte aqui, Guardián.");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("Zavala8");
            text.SetDefault("Do you feel that, Guardian? The Traveler's raw energies, scattered across this land.");
            text.AddTranslation(GameCulture.Polish, "Czujesz to Strażniku? Surowa energia Podróżnika, została rozproszona po całej tej krainie.");
            text.AddTranslation(GameCulture.Spanish, "¿Sientes eso, Guardián? La energia pura del Viajero, esparcida por esa tierra.");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("Zavala9");
            text.SetDefault("Guardian.");
            text.AddTranslation(GameCulture.Polish, "Strażnik.");
            text.AddTranslation(GameCulture.Spanish, "Guardián.");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("Zavala10");
            text.SetDefault("The Traveler graces us, Guardian.");
            text.AddTranslation(GameCulture.Polish, "Podróżnik łaskawi nas, Strażniku.");
            text.AddTranslation(GameCulture.Spanish, "El Viajero nos agradece, Guardián.");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("Zavala11");
            text.SetDefault("Let us begin.");
            text.AddTranslation(GameCulture.Spanish, "Empezemos.");
            text.AddTranslation(GameCulture.Polish, "Zacznijmy zatem.");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("Zavala12");
            text.SetDefault("Report, Guardian.");
            text.AddTranslation(GameCulture.Spanish, "Reportese, Guardián.");
            text.AddTranslation(GameCulture.Polish, "Raportuj, Strażniku.");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("Zavala13");
            text.SetDefault("The Darkness approaches, Guardian. We must be ready.");
            text.AddTranslation(GameCulture.Spanish, "La Oscuridad se acerca, Guardián. Debemos estar preparados.");
            text.AddTranslation(GameCulture.Polish, "Ciemność nadchodzi strażniku. Musimy być gotowi.");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("ZavalaBounty1");
            text.SetDefault("I'm still setting up shop, Guardian, but you're eager to get out there, aren't you? Alright, let's see what I can find...\nI need you to kill 100 Zombies, then report back to me for further orders.");
            text.AddTranslation(GameCulture.Spanish, "Sigo preparando la tienda, Guardián, pero estas ansioso por salir afuera, no? está bien, veamos que puedo encotrar...\nNecesito que elimines 100 Zombis, luego reportate aquí para próximas ordenes.");
            text.AddTranslation(GameCulture.Polish, "Nadal zakładam sklep, Strażniku ale nie możesz się doczekać żeby wyjść, prawda? W porządku, zobaczymy co uda mi się znaleźć...\nMusisz zabić 100 Zombie a następnie zgłoś się do mnie w celu uzyskania dalszych rozkazów.");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("ZavalaBounty2");
            text.SetDefault("Excellent work, Guardian! Take this as my appreciation for your hard work.");
            text.AddTranslation(GameCulture.Spanish, "¡Excelente trabajo, Guardián!");
            text.AddTranslation(GameCulture.Polish, "Znakomita robota Strażniku! Weź to jako zapłatę za twoją ciężka prace.");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("ZavalaBounty3");
            text.SetDefault("I've got another bounty for you, Guardian. The Dungeon is an evil place, filled with servants of the Darkness.\nI need you to slay 50 Skeletons to purge this infestation.");
            text.AddTranslation(GameCulture.Spanish, "La mazmorra es un lugar vil, lleno de sirvientes de la Oscuridad. Necesito que elimines 50 Esqueletos para purgar esta infestación.");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("ZavalaBounty4");
            text.SetDefault("I can feel the settling of the Darkness from here. You've done well, Guardian.");
            text.AddTranslation(GameCulture.Spanish, "Puedo sentir el desvanecimiento de la Oscuridad desde aquí. Hiciste un buen trabajo, Guardián.");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("ZavalaKilled1");
            text.SetDefault("Let's see here, Guardian. You've killed {0}/100 Zombies.");
            text.AddTranslation(GameCulture.Spanish, "Veamos, Guardián. Eliminaste {0}/100 Zombis.");
            text.AddTranslation(GameCulture.Polish, "Zobaczmy, Strażniku. Jak dotychczas zabiłeś {0}/100 Zombie.");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("ZavalaKilled2");
            text.SetDefault("Let's see here, Guardian. You've killed {0}/50 Skeletons.");
            text.AddTranslation(GameCulture.Spanish, "Veamos aquí, Guardian. Has matado (number)/50 Esqueletos.");
            text.AddTranslation(GameCulture.Polish, "Zobaczmy, Strażniku. Jak dotychczas zabiłeś {0}/50 Skeleton.");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("ZavalaGG");
            text.SetDefault("The Guardian Games are on, Guardian! Show everyone which class is the greatest.");
            text.AddTranslation(GameCulture.Spanish, "¡Los juegos de Guardianes han comenzado, Guardián! Muestrales a todos cual clase es la mejor.");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("ZavalaGGTitanWin");
            text.SetDefault("Through a fierce competition and defying all odds, my Titans have punched their way to victory in this year's Guardian Games. Make no mistake; the determination of both the Warlocks and Hunters were admirable. To commemorate, here is some Titan-themed decor. Stay strong, Guardian.");
            text.AddTranslation(GameCulture.Spanish, "A través de una dura competición y desafiando todas las posibilidades, mis Titanes golpearon hasta la victoria en los Juegos de Guardianes este año. No te equivoques, la determinación de los Hechiceros y Cazadores fue admirable. Para conmemorar, aquí tienes un poco de decoración de Titán. Mantenete fuerte, Guardián.");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("ZavalaGGHunterWin");
            text.SetDefault("Through a fierce competition and defying all odds, the Hunters have stealthily taken the win in this year's Guardian Games. Make no mistake; the determination of the Hunters was admirable, but us Titans will achieve victory next year. To commemorate, here is some Hunter-themed decor. Stay strong, Guardian.");
            text.AddTranslation(GameCulture.Spanish, "A través de una dura competición y desafiando todas las posibilidades, los Cazadores tomaron sigilosamente la victoria en los Juegos de Guardianes este año. No te equivoques, la determinación de los Hechiceros fue admirable, pero nosotros los Titanes alcanzaremos la victoria el proximo año. Para conmemorar, aquí tienes un poco de decoración de Cazador. Mantente fuerte, Guardián.");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("ZavalaGGWarlockWin");
            text.SetDefault("Through a fierce competition and defying all odds, the Warlocks have outsmarted every other class in this year's Guardian Games. Make no mistake; the determination of the Warlocks was admirable, but us Titans will achieve victory next year. To commemorate, here is some Warlock-themed decor. Stay strong, Guardian.");
            text.AddTranslation(GameCulture.Spanish, "A través de una dura competición y desafiando todas las posibilidades, los Hechiceros han burlado las demas clases en los Juegos de Guardianes este año. No te equivoques, la determinación de los Cazadores fue admirable, pero nosotros los Titanes alcanzaremos la victoria el proximo año. Para conmemorar, aquí tienes un poco de decoración de Hechicero. Mantente fuerte, Guardián.");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("Drifter1");
            text.SetDefault("How you livin'?");
            text.AddTranslation(GameCulture.Spanish, "¿Qué tal?");
            text.AddTranslation(GameCulture.Polish, "Jak się miewasz?");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("Drifter2");
            text.SetDefault("Get me those Motes and I'll make you rich, {0}, I promise.");
            text.AddTranslation(GameCulture.Spanish, "Traeme esas Motas y te haré rico, {0}, lo prometo");
            text.AddTranslation(GameCulture.Polish, "Daj mi te okruchy a ja zrobię z ciebie {0}, Obiecuje.");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("Drifter3");
            text.SetDefault("Call me Drifter.");
            text.AddTranslation(GameCulture.Spanish, "Llamame Vagabundo.");
            text.AddTranslation(GameCulture.Polish, "Zwij mnie dryfterem.");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("Drifter4");
            text.SetDefault("Ready to bang knuckles?");
            text.AddTranslation(GameCulture.Spanish, "¿Listo para chocar nudillos?");
            text.AddTranslation(GameCulture.Polish, "Gotowy na obijanie kostek?");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("Drifter5");
            text.SetDefault("Transmat firing!");
            text.AddTranslation(GameCulture.Spanish, "¡Teletransportación lista!");
            text.AddTranslation(GameCulture.Polish, "Wypalanie Transmatów!");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("Drifter6");
            text.SetDefault("Let's be bad guys.");
            text.AddTranslation(GameCulture.Spanish, "Seamos los chicos malos.");
            text.AddTranslation(GameCulture.Polish, "Bądźmy złymi chłopcami.");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("Drifter7");
            text.SetDefault("Motes of Light have always been a thing. Motes of Dark? I had to make 'em. One day, I may have to answer for that.");
            text.AddTranslation(GameCulture.Spanish, "Las Motas de Luz siempre existieron. ¿Motas de Oscuridad? Tengo que crearlas. Algún día, quizás tenga la respuesta a eso.");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("Drifter8");
            text.SetDefault("I see you lookin' at me like I'm nuts. You think all this is for nothing? That I do this cause I like it? You don't know the half of it.");
            text.AddTranslation(GameCulture.Spanish, "Veo que me miras como si estuviese loco. ¿Piensas que hago esto por nada?¿Que hago esto porqué quiero? No sabes nada.");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("Drifter9");
            text.SetDefault("Ah, all the stars in heaven! I am so... hungry.");
            text.AddTranslation(GameCulture.Spanish, "¡Ah, todas las estrellas en el cielo! Estoy tan... hambriento.");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("Drifter10");
            text.SetDefault("You think 'cause you released some fancy spirits, you're too good for me? Come on. Drifter needs his Motes.");
            text.AddTranslation(GameCulture.Spanish, "Piensas que porque liberaste algunos espiritus raros, ¿Eres demasiado bueno para mi? Vamos. El Vagabundo necesita sus Motas.");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("Drifter11");
            text.SetDefault("Light isn't the only source of power out here.");
            text.AddTranslation(GameCulture.Spanish, "La Luz no es la única fuente de poder ahí afuera.");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("Drifter12");
            text.SetDefault("Light, Dark... Let me tell you, the only thing that matters is the hand holding the gun.");
            text.AddTranslation(GameCulture.Spanish, "Luz, Oscuridad... Dejame decirte, lo unico que importa es la mano que sostenga el arma.");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("Drifter13");
            text.SetDefault("What happened? Did the Taken Take the sun or somethin'?!");
            text.AddTranslation(GameCulture.Spanish, "¿Qué ocurrió? ¡¿Acaso los poseidos poseyeron el sol o algo?!");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("Drifter14");
            text.SetDefault("Hey, {0}, wanna top off this party with some Gambit?");
            text.AddTranslation(GameCulture.Spanish, "Hey, {0}, ¿quieres animar esta escuadra con algo de gambito?");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("Drifter15");
            text.SetDefault("Oh, no, no, no, that CREEP is here again. When will him and these...\"Nine\" stop botherin' us, {0}?");
            text.AddTranslation(GameCulture.Spanish, "Oh, no, no, no, ese EXTRAÑO esta aqui devuelta. ¿Cuando el y esos... \"Nueve\" dejaran de molestarnos, {0}?");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("Drifter16");
            text.SetDefault("Gah, what's with {0}, {1}? He reminds me of ol' Zavala back at the Tower...what do you mean Zavala is here?!");
            text.AddTranslation(GameCulture.Spanish, "Gah, ¿que pasa con {0}, {1}? Me recuerda al viejo Zavala allá en la Torre... ¡¿Qué quieres decir con que Zavala está aquí ? !");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("Drifter17");
            text.SetDefault("Gah, what's with {0}, {1}? He reminds me of ol' Zavala back at the Tower, always killing all the fun...");
            text.AddTranslation(GameCulture.Spanish, "Gah, ¿que pasa con {0}, {1}? Me recuerda al viejo Zavala allá en la Torre, siempre matando la diversión...");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("Drifter18");
            text.SetDefault("We're being invaded! Find them before they find us.");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("DrifterMotes1");
            text.SetDefault("Thank you, {0}. I'll do something real special with these Motes, trust.");
            text.AddTranslation(GameCulture.Spanish, "Gracias, {0}. Haré algo realmente especial con estas motas, créeme.");
            text.AddTranslation(GameCulture.Polish, "Dziękuję {0}. Zrobię z tymi okruchami coś naprawde specialnego Zaufaj mi.");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("DrifterMotes2");
            text.SetDefault("Ooh, that's quite the haul you have there, thank you very much. I'ma do something real special with these Motes, somethin' that'll make you shiver.");
            text.AddTranslation(GameCulture.Spanish, "Ooh, ese es todo el botin que tienes ahí, muchas gracias. Haré algo muy especial con estas Motas, algo que te hará temblar.");
            text.AddTranslation(GameCulture.Polish, "Oo To niezła zdobycz, Dziękuję bardzo. zrobię coś naprawdę wyjątkowego z tymi okruchami, coś, co przyprawi Cię o dreszcze.");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("DrifterMotes3");
            text.SetDefault("Thanks for the Motes, {0}.");
            text.AddTranslation(GameCulture.Spanish, "Gracias por las Motas, {0}.");
            text.AddTranslation(GameCulture.Polish, "Dziękuję za okruchy {0}.");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("DrifterMotes4");
            text.SetDefault("Nice work. The line between Light and Dark's gettin' thinner every day. Keep walking it.");
            text.AddTranslation(GameCulture.Spanish, "Buen trabajo. La linea entre Luz y Oscuridad se vuelve mas fina cada día. Sigue así.");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("DrifterMotes5");
            text.SetDefault("Keep choosing the winning side, kid, and you'll keep on winning. Simple as that.");
            text.AddTranslation(GameCulture.Spanish, "Sigue eligiendo el lado ganador, muchacho, y seguirás ganando. Tan simple como eso.");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("DrifterMotes6");
            text.SetDefault("Hey {0}, thanks for the Motes. I said I'd make you rich, and I intend to keep that promise, unlike some others... Here, take this.");
            text.AddTranslation(GameCulture.Spanish, "Hey {0}, gracias por las Motas. He dicho que te haria rico, e intento mantener mi promesa, no como otros... aqui, ten esto.");
            text.AddTranslation(GameCulture.Polish, "Witaj {0}, dziękuję za okruchy. Mówiłem że zrobię cię bogatym i zamierzam dotrzymać tej obietnicy, nie tak jak niektórzy... Prosze weź to.");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("DrifterMotes7");
            text.SetDefault("Motes? Motes! Speaking of Motes, I've gotta way for you to get more, faster. This is for you, {0}.");
            text.AddTranslation(GameCulture.Spanish, "¿Motas? ¡Motas! Hablando de motas, conozco una forma de que consigas más, más rapido. esto es para ti, {0}.");
            text.AddTranslation(GameCulture.Polish, "Okruchy? Okruchy! Mówiąc okruchach mam dla ciebie sposób jak zdobyć więcej i to szybciej. To jest dla ciebie, {0}.");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("DrifterMotes8");
            text.SetDefault("I'll never get sick of Motes. Never. Anyways, here's your reward.");
            text.AddTranslation(GameCulture.Spanish, "I'll never get sick of Motes. Never. Anyways, here's your reward.");
            text.AddTranslation(GameCulture.Polish, "I'll never get sick of Motes. Never. Anyways, here's your reward.");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("DrifterMotes9");
            text.SetDefault("Mmm, Motes...ooh, I think you're gonna like this, {0}. Free merchandise, on the house!");
            text.AddTranslation(GameCulture.Spanish, "Mmm, Motas... ooh, pienso que esto te va a gustar, {0}. ¡Mercaderia gratis, en la casa!");
            text.AddTranslation(GameCulture.Polish, "Mmm okruchy, o myślę że to Ci się spodoba, {0}. Darmowy towar na koszt tego domu.");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("DrifterMotes10");
            text.SetDefault("Thanks for the Motes! By the way, I found these parts lying around. Maybe you could put 'em to good use, {0}?");
            text.AddTranslation(GameCulture.Spanish, "¡Gracias por las Motas! Por cierto, encontré estas partes por ahí. ¿Quizás podrias darle un buen uso, {0}?");
            text.AddTranslation(GameCulture.Polish, "Dzięki za okruchy! przy okazji znalazłem tę części po niewierające się tutaj. Może ty wykorzystasz je w sposób jaki zostały do tego stworzone {0}?");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("DrifterMotes11");
            text.SetDefault("These are some nice Motes...oh yeah, I found this Ghost on the ground. Not sure what use it may be to you, but, it's yours now, {0}.");
            text.AddTranslation(GameCulture.Spanish, "Estas son unas buenas Motas... oh cierto, encontré esta... cosa, el el suelo. no estoy seguro de que es, pero ahora es tuyo, {0}.");
            text.AddTranslation(GameCulture.Polish, "To są niezłe okruchy, a znalazłem to leżące na ziemi. Nie jestem pewien co to jest ale teraz należy to do ciebie {0}.");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("DrifterMotes12");
            text.SetDefault("Motes, {0}, Motes! Gotta unique weapon I hand-crafted just for you. Take care of it.");
            text.AddTranslation(GameCulture.Spanish, "¡Motas, {0}, Motas! Tengo un arma que hice para ti, cuidala.");
            text.AddTranslation(GameCulture.Polish, "Motes, {0}, Motes! Gotta unique weapon I hand-crafted just for you. Take care of it.");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("DrifterMotes13");
            text.SetDefault("{0}, you gotta have those Motes on you! Come back when you got some.");
            text.AddTranslation(GameCulture.Spanish, "{0}, ¡tienes que tener las Motas contigo! Vuelve cuando tengas algunas.");
            text.AddTranslation(GameCulture.Polish, "{0}, you gotta have those Motes on you! Come back when you got some.");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("DrifterMotes14");
            text.SetDefault("Thanks for the...huh? You don't have any Motes for me to unload off 'ya!");
            text.AddTranslation(GameCulture.Spanish, "Gracias por las... ¿Heh? ¡No tienes ningunas Motas para desocuparme de ti!");
            text.AddTranslation(GameCulture.Polish, "Thanks for the...huh? You don't have any Motes for me to unload off 'ya!");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("DrifterMotes15");
            text.SetDefault("Hey, you gotta have Motes to deposit! You tryna cheat me? Just kidding, {0}.");
            text.AddTranslation(GameCulture.Spanish, "¡Hey, tienes que depositar las Motas en el deposito! ¿Estás intentando estafarme? Es broma, {0}.");
            text.AddTranslation(GameCulture.Polish, "Hey, you gotta have Motes to deposit! You tryna cheat me? Just kidding, {0}.");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("DrifterMotes16");
            text.SetDefault("Aw man, {0}, you haven't deposited any Motes yet!");
            text.AddTranslation(GameCulture.Spanish, "¡Cielos, {0}, no depositaste Motas todavia!");
            text.AddTranslation(GameCulture.Polish, "O człowieku, {0}, jeszcze nie zdepozytowałeś jeszcze żadnych okruchów.");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("DrifterMotes17");
            text.SetDefault("Woo, you've deposited {0} Motes! This is one heckuva collection, {1}.");
            text.AddTranslation(GameCulture.Spanish, "¡Woo, depositaste {0} Motas! Esta es una gran collección, {1}.");
            text.AddTranslation(GameCulture.Polish, "Woo, Zdepozytowałeś {0} okruchy. to jest jedna cholernie duża kolekcja {1}.");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("DrifterMotes18");
            text.SetDefault("You've deposited {0} Motes so far, {1}. Not bad.");
            text.AddTranslation(GameCulture.Spanish, "Depositaste {0} Motas por el momento, {1}. Nada mal.");
            text.AddTranslation(GameCulture.Polish, "Zdepozytowałeś {0} jak narazie, {1}. Nieźle.");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("DrifterMotes19");
            text.SetDefault("You've deposited a total of {0} Motes, {1}.");
            text.AddTranslation(GameCulture.Spanish, "Depositaste un total de {0} Motas, {1}.");
            text.AddTranslation(GameCulture.Polish, "Zdepozytowałeś w sumie {0} Okruchów, {1}.");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("DrifterRepeatable1");
            text.SetDefault("Here's your reward, {0}. Use it to get me more Motes!");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("DrifterRepeatable2");
            text.SetDefault("Awesome, {0}. This'll get you more Motes.");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("Brother");
            text.SetDefault("brother");
            text.AddTranslation(GameCulture.Spanish, "hermano");
            text.AddTranslation(GameCulture.Polish, "bracie");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("Sister");
            text.SetDefault("sister");
            text.AddTranslation(GameCulture.Spanish, "hermana");
            text.AddTranslation(GameCulture.Polish, "siostro");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("Bounty");
            text.SetDefault("Bounty");
            text.AddTranslation(GameCulture.Spanish, "Recompensa");
            text.AddTranslation(GameCulture.Polish, "Nagroda");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("Decrypt");
            text.SetDefault("Decrypt");
            text.AddTranslation(GameCulture.Spanish, "Desencriptar");
            text.AddTranslation(GameCulture.Polish, "Odszyfruj"); //taken off G translate
            mod.AddTranslation(text);

            text = mod.CreateTranslation("GiveMotes");
            text.SetDefault("Give Motes");
            text.AddTranslation(GameCulture.Polish, "Daj okruchy");
            text.AddTranslation(GameCulture.Spanish, "Dar Motas");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("CheckMotes");
            text.SetDefault("Check Motes");
            text.AddTranslation(GameCulture.Polish, "Sprawdź okruchy");
            text.AddTranslation(GameCulture.Spanish, "Inspeccionar Motas");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("SuperCharge");
            text.SetDefault("Super charged!");
            text.AddTranslation(GameCulture.Spanish, "¡Súper cargada!");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("SuperInventory");
            text.SetDefault("You must have an inventory slot free to activate your super.");
            text.AddTranslation(GameCulture.Spanish, "Debes tener un espacio del inventario vacio para activar tu súper.");
            mod.AddTranslation(text);

            text = mod.CreateTranslation("VaultOfGlass");
            text.SetDefault("Vault of Glass");
            text.AddTranslation(GameCulture.Spanish, "Vault of Glass");
            mod.AddTranslation(text);
        }

        public void IAutoloadable_PostSetUpContent() { }

		public void IAutoloadable_Unload() { }
	}
}