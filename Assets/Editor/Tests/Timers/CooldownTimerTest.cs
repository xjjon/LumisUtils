using Assets.Scripts.Timers;
using NUnit.Framework;

[TestFixture]
public class CooldownTimerTest
{

    private CooldownTimer _cooldownTimer;

    [SetUp]
    public void SetUp()
    {
        _cooldownTimer = new CooldownTimer(1, false);
        _cooldownTimer.TimerCompleteEvent += () => { Assert.IsTrue(_cooldownTimer.Completed); };
    }

    [Test]
    public void NormalNonRecurringTest()
    {
        Assert.IsFalse(_cooldownTimer.IsActive);
        Assert.IsFalse(_cooldownTimer.IsReccuring);
        Assert.IsFalse(_cooldownTimer.Completed);
        Assert.AreEqual(1, _cooldownTimer.TimeRemaining);
        Assert.AreEqual(1, _cooldownTimer.TotalTime);
        Assert.AreEqual(0, _cooldownTimer.TimeElapsed);
        Assert.AreEqual(0, _cooldownTimer.TimesCounted);

        _cooldownTimer.Start();

        Assert.IsTrue(_cooldownTimer.IsActive);
        Assert.IsFalse(_cooldownTimer.IsReccuring);
        Assert.IsFalse(_cooldownTimer.Completed);
        Assert.AreEqual(1, _cooldownTimer.TimeRemaining);
        Assert.AreEqual(1, _cooldownTimer.TotalTime);
        Assert.AreEqual(0, _cooldownTimer.TimeElapsed);
        Assert.AreEqual(0, _cooldownTimer.TimesCounted);

        _cooldownTimer.Update(1);

        Assert.IsFalse(_cooldownTimer.IsActive);
        Assert.IsFalse(_cooldownTimer.IsReccuring);
        Assert.IsTrue(_cooldownTimer.Completed);
        Assert.AreEqual(0, _cooldownTimer.TimeRemaining);
        Assert.AreEqual(1, _cooldownTimer.TotalTime);
        Assert.AreEqual(1, _cooldownTimer.TimeElapsed);
        Assert.AreEqual(1, _cooldownTimer.TimesCounted);

        _cooldownTimer.Update(1);

        Assert.IsFalse(_cooldownTimer.IsActive);
        Assert.IsFalse(_cooldownTimer.IsReccuring);
        Assert.IsTrue(_cooldownTimer.Completed);
        Assert.AreEqual(0, _cooldownTimer.TimeRemaining);
        Assert.AreEqual(1, _cooldownTimer.TotalTime);
        Assert.AreEqual(1, _cooldownTimer.TimeElapsed);
        Assert.AreEqual(1, _cooldownTimer.TimesCounted);
    }

    [Test]
    public void NormalRecurringTest()
    {
        _cooldownTimer = new CooldownTimer(1, true);

        Assert.IsFalse(_cooldownTimer.IsActive);
        Assert.IsTrue(_cooldownTimer.IsReccuring);
        Assert.IsFalse(_cooldownTimer.Completed);
        Assert.AreEqual(1, _cooldownTimer.TimeRemaining);
        Assert.AreEqual(1, _cooldownTimer.TotalTime);
        Assert.AreEqual(0, _cooldownTimer.TimeElapsed);
        Assert.AreEqual(0, _cooldownTimer.TimesCounted);

        _cooldownTimer.Start();

        Assert.IsTrue(_cooldownTimer.IsActive);
        Assert.IsTrue(_cooldownTimer.IsReccuring);
        Assert.IsFalse(_cooldownTimer.Completed);
        Assert.AreEqual(1, _cooldownTimer.TimeRemaining);
        Assert.AreEqual(1, _cooldownTimer.TotalTime);
        Assert.AreEqual(0, _cooldownTimer.TimeElapsed);
        Assert.AreEqual(0, _cooldownTimer.TimesCounted);

        _cooldownTimer.Update(1);

        Assert.IsTrue(_cooldownTimer.IsActive);
        Assert.IsTrue(_cooldownTimer.IsReccuring);
        Assert.IsFalse(_cooldownTimer.Completed);
        Assert.AreEqual(1, _cooldownTimer.TimeRemaining);
        Assert.AreEqual(1, _cooldownTimer.TotalTime);
        Assert.AreEqual(0, _cooldownTimer.TimeElapsed);
        Assert.AreEqual(1, _cooldownTimer.TimesCounted);

        _cooldownTimer.Update(0.5f);

        Assert.IsTrue(_cooldownTimer.IsActive);
        Assert.IsTrue(_cooldownTimer.IsReccuring);
        Assert.IsFalse(_cooldownTimer.Completed);
        Assert.AreEqual(0.5f, _cooldownTimer.TimeRemaining);
        Assert.AreEqual(1, _cooldownTimer.TotalTime);
        Assert.AreEqual(0.5f, _cooldownTimer.TimeElapsed);
        Assert.AreEqual(1, _cooldownTimer.TimesCounted);
    }

    [Test]
    public void AddTimeTest()
    {
        _cooldownTimer.Start();
        _cooldownTimer.Update(0.5f);

        Assert.IsTrue(_cooldownTimer.IsActive);
        Assert.IsFalse(_cooldownTimer.IsReccuring);
        Assert.IsFalse(_cooldownTimer.Completed);
        Assert.AreEqual(0.5f, _cooldownTimer.TimeRemaining);
        Assert.AreEqual(1, _cooldownTimer.TotalTime);
        Assert.AreEqual(0.5f, _cooldownTimer.TimeElapsed);
        Assert.AreEqual(0, _cooldownTimer.TimesCounted);

        _cooldownTimer.AddTime(0.5f);

        Assert.IsTrue(_cooldownTimer.IsActive);
        Assert.IsFalse(_cooldownTimer.IsReccuring);
        Assert.IsFalse(_cooldownTimer.Completed);
        Assert.AreEqual(1, _cooldownTimer.TimeRemaining);
        Assert.AreEqual(1.5f, _cooldownTimer.TotalTime);
        Assert.AreEqual(0.5f, _cooldownTimer.TimeElapsed);
        Assert.AreEqual(0, _cooldownTimer.TimesCounted);

        _cooldownTimer.Update(1);

        Assert.IsFalse(_cooldownTimer.IsActive);
        Assert.IsFalse(_cooldownTimer.IsReccuring);
        Assert.IsTrue(_cooldownTimer.Completed);
        Assert.AreEqual(0, _cooldownTimer.TimeRemaining);
        Assert.AreEqual(1.5f, _cooldownTimer.TotalTime);
        Assert.AreEqual(1.5f, _cooldownTimer.TimeElapsed);
        Assert.AreEqual(1, _cooldownTimer.TimesCounted);
    }

}