using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using TestNinja.Mocking;
using Xunit;

namespace TestNinja.UnitTests.Mocking
{
    public class HousekeeperServiceTests
    {
        private readonly HousekeeperService _service;
        private readonly Mock<IStatementGenerator> _statementGenerator;
        private readonly Mock<IEmailSender> _emailSender;
        private readonly Mock<IXtraMessageBox> _messageBox;
        private DateTime _statementDate = new DateTime(2020, 1, 1);
        private readonly Housekeeper _housekeeper;

        public HousekeeperServiceTests()
        {
            _housekeeper = new Housekeeper { Email = "a", Oid = 1, FullName = "b", StatementEmailBody = "body" };
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(uow => uow.Query<Housekeeper>()).Returns(new List<Housekeeper>
            {
                _housekeeper
            }.AsQueryable());

            _statementGenerator = new Mock<IStatementGenerator>();
            _emailSender = new Mock<IEmailSender>();
            _messageBox = new Mock<IXtraMessageBox>();

            _service = new HousekeeperService(unitOfWork.Object, _statementGenerator.Object, _emailSender.Object, _messageBox.Object);;
        }

        [Fact]
        public void SendStatementEmails_WhenCalled_GenerateStatements()
        {
            _service.SendStatementEmails(_statementDate);

            _statementGenerator.Verify(sg => sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate));
        }

        [Fact]
        public void SendStatementEmails_HouseKeeperEmailIsNull_ShouldNotGenerateStatements()
        {
            _housekeeper.Email = null;

            _service.SendStatementEmails(_statementDate);

            _statementGenerator.Verify(sg => sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate), Times.Never);
        }
    }
}