using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace MentalBreakBlocker {
    public class ModExtension_MentalBreakBlocker : DefModExtension {
        public BlockMentalBreakCause cause = BlockMentalBreakCause.all;

        public bool IsBlocked(bool causedByMood, bool causedByDamage, bool causedByPsycast) {
            return
                (((byte)cause) & (
                (causedByMood ? 1 : 0) | (causedByDamage ? 2 : 0) | (causedByPsycast ? 4 : 0))
                ) > 0;
        }
    }
    public enum BlockMentalBreakCause : byte {
        mood = 1,
        damage = 2,
        psycast = 4,

        moodAndDamage = 3,
        moodAndPsycast = 5,
        damageAndPsycast = 6,

        all = 7
    }
}
