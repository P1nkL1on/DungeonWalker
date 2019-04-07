using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    public class Properties
    {
        public string name;
    }
    public class CanDie
    {
        protected int hp;
        protected int hpmax;
    }

    public class AlsoCanAttack : CanDie
    {
        protected int damage;
    }

    public class AlsoHasStats : AlsoCanAttack
    {
        protected int _stamina;  // increase max poise (poise descreases on attacking and hits taken (even more))
                                 // when poise ends unit will be confused for a turn
        protected int _vitality; // increasing max health possible and weigth carrying potential
        protected int _stretngth;  // increases attack damage and damage resistance
        protected int _knowledge;  // improoves abilities to solve puzzles, unlocking doors, reading books, using technics etc
        protected int _wisdom;     // inceases a manapool and a spell efficiency

    }
}
