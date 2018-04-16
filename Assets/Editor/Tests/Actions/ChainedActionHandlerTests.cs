using System.Collections.Generic;
using Assets.Scripts.Actions;
using Moq;
using NUnit.Framework;

[TestFixture]
public class ChainedActionHandlerTest
{
    private ChainedActionHandler _chainedActionHandler;

    private Mock<IActionHandler> _mockedActionHandler1;
    private Mock<IActionHandler> _mockedActionHandler2;

    [Test]
    public void ExecuteFailOnFirstActionTest()
    {
        _mockedActionHandler1 = MockActionHandler(false);
        _mockedActionHandler2 = MockActionHandler(true);
        _chainedActionHandler = new ChainedActionHandler(
            new List<IActionHandler>(
                new[] { _mockedActionHandler1.Object, _mockedActionHandler2.Object }));

        var result = _chainedActionHandler.Execute();

        Assert.IsFalse(result);
        _mockedActionHandler1.Verify(x => x.Execute(), Times.Exactly(1));
        _mockedActionHandler2.Verify(x => x.Execute(), Times.Never);
    }

    [Test]
    public void ExecuteSuceedAllActionsTest()
    {
        _mockedActionHandler1 = MockActionHandler(true);
        _mockedActionHandler2 = MockActionHandler(true);
        _chainedActionHandler = new ChainedActionHandler(
            new List<IActionHandler>(
                new[] { _mockedActionHandler1.Object, _mockedActionHandler2.Object }));

        var result = _chainedActionHandler.Execute();

        Assert.IsTrue(result);
        _mockedActionHandler1.Verify(x => x.Execute(), Times.Exactly(1));
        _mockedActionHandler2.Verify(x => x.Execute(), Times.Exactly(1));
    }

    private Mock<IActionHandler> MockActionHandler(bool state)
    {
        var actionHandler = new Mock<IActionHandler>();
        actionHandler.Setup(x => x.Execute()).Returns(state);
        return actionHandler;
    }
}