﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnglishBot.Models.DbModels
{
    public enum Status
    {
        NewUser = 0,
        WaitingNewUser,
        TranslateRuToEn,
        WaitingTranslteRuToEn,
        TranslateEnToRu,
        WaitingTranslteEnToRu,
        GetPhraseologiсal,
        GetWord,
        WaitingGetWord,
        other
    }
}