using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaoRemote
{
    class BehaviorSequence : List<string>
    {
        private enum Direction { LEFT, RIGHT };
        private enum Cue { NO_CUE, CUE };
        private enum Action { POINT, PUSH }; 

        private const string BASE_DIR = "contingency/";
        private const string LOOK_BEHAVIOR = BASE_DIR + "look";
        private const string CUE_BEHAVIOR = BASE_DIR + "cue";
        private const string NO_CUE_BEHAVIOR = BASE_DIR + "no_cue";
        private const string POINT_RIGHT_BEHAVIOR = BASE_DIR + "point_right";
        private const string POINT_LEFT_BEHAVIOR = BASE_DIR + "point_left";
        private const string PUSH_RIGHT_BEHAVIOR = BASE_DIR + "push_right";
        private const string PUSH_LEFT_BEHAVIOR = BASE_DIR + "push_left";

        private Direction MyDirection;
        private Cue MyCue;
        private Action MyAction;

        private BehaviorSequence(Direction d, Cue c, Action a) 
            : this()
        {
            MyDirection = d;
            MyCue = c;
            MyAction = a;
        }

        private BehaviorSequence()
            : base()
        {
        }

        public string GetName()
        {
            return MyAction.ToString() + "-" + MyCue.ToString() + "-" + MyDirection.ToString();
        }

        private static BehaviorSequence constructBehaviorSequence(Cue c, Direction d, Action a)
        {
            BehaviorSequence sec = new BehaviorSequence(d,c,a);
            sec.Add(LOOK_BEHAVIOR);
            switch (c)
            {
                case Cue.NO_CUE: sec.Add(NO_CUE_BEHAVIOR); break;
                case Cue.CUE: sec.Add(CUE_BEHAVIOR); break;
            }
            switch (d)
            {
                case Direction.RIGHT:
                    switch (a)
                    {
                        case Action.POINT: sec.Add(POINT_RIGHT_BEHAVIOR); break;
                        case Action.PUSH: sec.Add(PUSH_RIGHT_BEHAVIOR); break;
                    } break;
                case Direction.LEFT:
                    switch (a)
                    {
                        case Action.POINT: sec.Add(POINT_LEFT_BEHAVIOR); break;
                        case Action.PUSH: sec.Add(PUSH_LEFT_BEHAVIOR); break;
                    } break;
            }
            return sec;
        }

        public static BehaviorSequence PointLeftNoCueSequence() {
            return constructBehaviorSequence(Cue.NO_CUE, Direction.LEFT, Action.POINT);
        }

        public static BehaviorSequence PointLeftCueSequence()
        {
            return constructBehaviorSequence(Cue.CUE, Direction.LEFT, Action.POINT);
        }

        public static BehaviorSequence PushLeftNoCueSequence()
        {
            return constructBehaviorSequence(Cue.NO_CUE, Direction.LEFT, Action.PUSH);
        }

        public static BehaviorSequence PushLeftCueSequence()
        {
            return constructBehaviorSequence(Cue.CUE, Direction.LEFT, Action.PUSH);
        }

        public static BehaviorSequence PointRightNoCueSequence()
        {
            return constructBehaviorSequence(Cue.NO_CUE, Direction.RIGHT, Action.POINT);
        }

        public static BehaviorSequence PointRightCueSequence()
        {
            return constructBehaviorSequence(Cue.CUE, Direction.RIGHT, Action.POINT);
        }

        public static BehaviorSequence PushRightNoCueSequence()
        {
            return constructBehaviorSequence(Cue.NO_CUE, Direction.RIGHT, Action.PUSH);
        }

        public static BehaviorSequence PushRightCueSequence()
        {
            return constructBehaviorSequence(Cue.CUE, Direction.RIGHT, Action.PUSH);
        }

        internal static BehaviorSequence EmptyBehaviorSequence()
        {
            return new BehaviorSequence();
        }
    }
}
