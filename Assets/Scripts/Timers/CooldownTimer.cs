namespace Assets.Scripts.Timers
{
    /*
     * Utility timer. Has the option to be recurring.
     * Update needs to be called with the deltaTime.
     * TimerCompleteEvent is emitted on completion.
    */
    public class CooldownTimer
    {
        public float TimeRemaining { get; private set; }
        public float TotalTime { get; private set; }
        public bool IsReccuring { get; private set; }
        public bool IsActive { get; private set; }
        public int TimesCounted { get; private set; }

        public float TimeElapsed
        {
            get { return TotalTime - TimeRemaining; }
        }

        public bool Completed
        {
            get
            {
                return TimeRemaining <= 0;
            }
        }

        public float PercentElapsed
        {
            get { return TimeElapsed / TotalTime; }
        }

        public delegate void TimerCompleteHandler();

        /// <summary>
        /// Emmits event when timer is completed
        /// </summary>
        public event TimerCompleteHandler TimerCompleteEvent;

        /// <summary>
        /// Create a new CooldownTimer
        /// Must call Start() to begin timer
        /// </summary>
        /// <param name="time">Timer length (seconds)</param>
        /// <param name="reccuring">Is this timer reccuring</param>
        public CooldownTimer(float time, bool reccuring)
        {
            TotalTime = time;
            IsReccuring = reccuring;
            TimeRemaining = TotalTime;
        }

        /// <summary>
        /// Start timer with existing time
        /// </summary>
        public void Start()
        {
            if (IsActive) { TimesCounted++; }
            TimeRemaining = TotalTime;
            IsActive = true;
            if (TimeRemaining <= 0)
                if (TimerCompleteEvent != null) TimerCompleteEvent();
        }

        /// <summary>
        /// Start timer with time
        /// </summary>
        /// <param name="time"></param>
        public void Start(float time)
        {
            TotalTime = time;
            Start();
        }

        public virtual void Update(float timeDelta)
        {
            if (TimeRemaining > 0 && IsActive)
            {
                TimeRemaining -= timeDelta;
                if (TimeRemaining <= 0)
                {
                    if (TimerCompleteEvent != null) TimerCompleteEvent();
                    TimesCounted++;
                    if (IsReccuring)
                    {
                        TimeRemaining = TotalTime;
                    }
                    else
                    {
                        IsActive = false;
                    }
                }
            }
        }

        public void Invoke()
        {
            if (TimerCompleteEvent != null) TimerCompleteEvent();
        }

        public void Pause()
        {
            IsActive = false;
        }

        public void Stop()
        {
            Pause();
        }

        public void AddTime(float time)
        {
            TimeRemaining += time;
            TotalTime += time;
        }
    }
}
