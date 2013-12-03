using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NaoRemote
{
    class TrialSequence : List<BehaviorSequence>
    {
        private string Name;
        private int nTrials;

        private TrialSequence() : base() 
        {
            Name = "Default";
        }

        public void Shuffle()
        {
            int n = this.Count;
            while (n > 1)
            {
                n--;
                int k = ThreadSafeRandom.ThisThreadsRandom.Next(n + 1);
                BehaviorSequence value = this[k];
                this[k] = this[n];
                this[n] = value;
            }
        }

        public string GetName()
        {
            return Name;
        }

        public int TrialNumber()
        {
            return nTrials - Count;
        }

        static public TrialSequence CreateEmptyTrialSequence()
        {
            return new TrialSequence();
        }

        static public TrialSequence CreateUnpredictiveTrialSequence()
        {
            TrialSequence seq = new TrialSequence();
            
            //behavioral cue on, push
            seq.Add(BehaviorSequence.PointRightCueSequence());

            //behavioral cue on, point
            for(int i = 0; i < 3; i +=1)
            {
				seq.Add(BehaviorSequence.PointLeftCueSequence());
                seq.Add(BehaviorSequence.PointRightCueSequence());
				
            }

            //behavioral cue off, push
            for(int i = 0; i < 3; i +=1)
			{
            	seq.Add(BehaviorSequence.PushLeftNoCueSequence());
                seq.Add(BehaviorSequence.PushRightNoCueSequence());
            }
            
            //behavioral cue off, point
            for (int i = 0; i < 18; i += 1)
            {
                seq.Add(BehaviorSequence.PointLeftNoCueSequence());
                seq.Add(BehaviorSequence.PointRightNoCueSequence());
            }
            
            seq.nTrials = seq.Count;
            seq.Name = "Unpredictive";
            seq.Shuffle();
            return seq;
        }

        static public TrialSequence CreatePredictiveTrialSequence()
        {
            TrialSequence seq = new TrialSequence();
            
            //behavioral cue on, push
            for(int i = 0; i < 3; i +=1)
            {
                seq.Add(BehaviorSequence.PushLeftCueSequence());
                seq.Add(BehaviorSequence.PushRightCueSequence());
            }
            
            //behavioral cue on, point
            seq.Add(BehaviorSequence.PointRightCueSequence());
            
            //behavioral cue off, push
            seq.Add(BehaviorSequence.PushLeftNoCueSequence());
            
            //behavioral cue off, point
            for (int i = 0; i < 20; i += 1)
            {
                seq.Add(BehaviorSequence.PointLeftNoCueSequence());
                seq.Add(BehaviorSequence.PointRightNoCueSequence());
            }
            seq.Add(BehaviorSequence.PointLeftNoCueSequence());
            
            
            seq.nTrials = seq.Count;
            seq.Name = "Predictive";
            seq.Shuffle();
            return seq;
        }

        public static class ThreadSafeRandom
        {
            [ThreadStatic]
            private static Random Local;

            public static Random ThisThreadsRandom
            {
                get { return Local ?? (Local = new Random(unchecked(Environment.TickCount * 31 + Thread.CurrentThread.ManagedThreadId))); }
            }
        }

        static class MyExtensions
        {
            
        }
    }
}
