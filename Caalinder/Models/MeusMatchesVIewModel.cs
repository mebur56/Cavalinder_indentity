﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Caalinder.Models
{
    public class MeusMatchesVIewModel
    {
        public int MatchId { get; set; }
        public HorseModel MeusCavalos { get; set; }
        public HorseModel CavalosDeles { get; set; }
        public bool Relike;

    }
    public class MeusMatchesVIewIndex
    {
        public List<MeusMatchesVIewModel> MeusMatchesList = new List<MeusMatchesVIewModel>();

    }

    public class MatchViewDetails
    {
        public HorseModel Cavalo { get; set; }
        public string Email { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
    }
}