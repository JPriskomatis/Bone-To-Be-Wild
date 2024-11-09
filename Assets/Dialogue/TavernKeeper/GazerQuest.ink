EXTERNAL ActivateQuestPanel()
->Intro



===Intro===
Welcome to the Pyrohydra, stranger!
Don't mind the smell – that’s just the ‘special’ stew.
    ->Callback
===Callback===
What can I do for you skinny fella?
    *[Do you have any quests?]
        ->GazerQuest
    *[Can you give me information about the town?]
        ->Information
    *[I don't need anything]
        ->EndDialogue

===Information===
well, who do you want to learn more about?
            **Lord of Acton
                Armodeus, the Brass is the lord of Acton.
                A powerful dragonborn that rose to lordeship through many victorius battles.
                He is a fair lord, that I must say, often going the extra mile to take care of the peasants.
                Not always that easy, as monsters seem to be everywhere these days.
                ->Callback
            **Powerful individuals
                yes
                ->Callback
            **Monster situation
                yes
                ->Callback

===GazerQuest===
Well yes... but its not for the faint of heart
and looking at you, I am not sure if you can handle it.
    *[Just tell me]
        Ah, they say it’s a god-forsaken creature, cursed and foul as sin itself.
        One look from that wicked eye, and your bones'll feel like ice, trust me.
        Folk who’ve seen it swear it leaves a trail of rot in its wake – enough to make even the bravest men shudder.
        Some have only seen it, less than a handful has dared to fight it.
        My advice? no ammount of gold can be worth your life, if you even got that.
            **[Who has fought it?]
                I don't know son, but I'm sure you can find him.
                ***[Give me the quest details]
                    Arlight
                    ~ ActivateQuestPanel()
                ->DONE
                    **[Give me the quest details]
                Alright.
                ->DONE
    *[You are right]
        ->Callback
                
===EndDialogue===
Farewell skeleton
->DONE