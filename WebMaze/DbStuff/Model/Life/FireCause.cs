using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMaze.DbStuff.Model.Life
{
    public enum FireCauseEnum
    {
        NotAvailable = 0,
        unknown = 100,
        lightning = 201,
        volcanism = 202,
        gasEmission = 203,
        electicalPower = 301,
        railroads = 302,
        vehicles = 303,
        works = 304,
        weapons = 305,
        selfIgnition = 306,
        vegetationManagement = 411,
        wasteManagement = 413,
        recreation = 414,
        fireworks = 421,
        cigarettes = 422,
        hotAshes = 423,
        interestProfit = 511,
        conflictRevenge = 512,
        vandalism = 513,
        excitement = 514,
        crimeConcealment = 515,
        extremist = 516,
        mentalIllnes = 521,
        children = 522,
        rekindle = 600,
    }
}
