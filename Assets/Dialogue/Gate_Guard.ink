-> main
EXTERNAL OpenGate()


=== main ===

Gate guard: Welcome to the great town of Acton, we ho- ... Wait a damn minute, are you a skeleton? I guess the town is open.



+ [What can you tell me about this city?]
    Go on, I don't have anything more to say to you.
    ++ [But I don't know what happened to me.]
        ...
        ~ OpenGate()
        -> DONE
        
    ++ [But please, I need help.]
        ...
        -> DONE
    ++ [Damn you are useless.]
        Watch your tongue or I'll slice it off of you.
        -> DONE

+ [Is something wrong with that?]
    Go on, I don't have anything more to say to you.
    ++ [But I don't know what happened to me.]
        ...
        -> DONE
    ++ [But please, I need help.]
        ...
        -> DONE
    ++ [Damn you are useless.]
        Watch your tongue or I'll slice it off of you.
        -> DONE

-> END
