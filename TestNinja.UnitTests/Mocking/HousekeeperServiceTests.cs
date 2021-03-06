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
        private string _statementFileName;

        public HousekeeperServiceTests()
        {
            _housekeeper = new Housekeeper { Email = "a", Oid = 1, FullName = "b", StatementEmailBody = "body" };
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(uow => uow.Query<Housekeeper>()).Returns(new List<Housekeeper>
            {
                _housekeeper
            }.AsQueryable());

            _statementFileName = "fileName";
            _statementGenerator = new Mock<IStatementGenerator>();
            _statementGenerator
                .Setup(sg => sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate))
                .Returns(() => _statementFileName);

            _emailSender = new Mock<IEmailSender>();
            _messageBox = new Mock<IXtraMessageBox>();

            _service = new HousekeeperService(unitOfWork.Object, _statementGenerator.Object, _emailSender.Object, _messageBox.Object); ;
        }

        [Fact]
        public void SendStatementEmails_WhenCalled_GenerateStatements()
        {
            _service.SendStatementEmails(_statementDate);

            _statementGenerator.Verify(sg => sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate));
        }

        [Theory]
        [InlineData(null)]
        [InlineData(" ")]
        [InlineData("")]
        public void SendStatementEmails_InvalidHouseKeeperEmail_ShouldNotGenerateStatements(String email)
        {
            _housekeeper.Email = email;

            _service.SendStatementEmails(_statementDate);

            _statementGenerator.Verify(sg => sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate), Times.Never);
        }

        [Fact]
        public void SendStatementEmails_WhenCalled_ShouldEmailStatement()
        {
            _service.SendStatementEmails(_statementDate);

            _emailSender.Verify(es => es.EmailFile(_housekeeper.Email, _housekeeper.StatementEmailBody, _statementFileName, It.IsAny<string>()));
        }


        [Theory]
        [InlineData(null)]
        [InlineData(" ")]
        [InlineData("")]
        public void SendStatementEmails_InvalidFileName_ShouldNotEmailStatement(string fileName)
        {
            _statementFileName = fileName;

            _service.SendStatementEmails(_statementDate);

            _emailSender.Verify(es => es.EmailFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void SendStatementEmails_EmailSendingFails_DisplayMessageBox()
        {
            _emailSender.Setup(es => es.EmailFile(
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()
                )).Throws<Exception>();
            
            _service.SendStatementEmails(_statementDate);

            _messageBox.Verify(mb => mb.Show(It.IsAny<string>(), It.IsAny<string>(), MessageBoxButtons.OK));
        }
    }
}