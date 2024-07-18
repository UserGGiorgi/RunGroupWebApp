using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RunGroupWebApp.Controllers;
using RunGroupWebApp.Interfaces;
using RunGroupWebApp.Models;
using RunGroupWebApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunGroopWebApp.Tests.Controller
{
    public class ClubControllerTests
    {
        private IClubRepository _clubRepository; 
        private IPhotoService _photoService;
        private IHttpContextAccessor _IHttpContextAccessor;
        private ClubController _clubController;

        public ClubControllerTests()
        {
            //Dependancy
            _clubRepository = A.Fake<IClubRepository>();
            _photoService = A.Fake<IPhotoService>();
            _IHttpContextAccessor=A.Fake<IHttpContextAccessor>();
            //SUT
            _clubController=new ClubController(_clubRepository,_photoService,_IHttpContextAccessor);



        }
        [Fact]
        public void ClubController_Index_ReturnsSeccess()
        {
            //Arrange
            var clubs=A.Fake<IEnumerable<Club>>();    
            A.CallTo(()=>_clubRepository.GetAll()).Returns(clubs);
            //Act
            var result=_clubController.Index();


            //Assert
            result.Should().BeOfType<Task<IActionResult>>();
        }
        [Fact]  
        public void ClubController_Detail_ReturnsSuccess()
        {
            //arrange
            var id = 1;
            var club=A.Fake<Club>();
            A.CallTo(()=>_clubRepository.GetByIdAsync(id)).Returns(club);

            //Act
            var result = _clubController.Detail(id);


            //Assert
            result.Should().BeOfType(typeof(Task<IActionResult>));

        }

    }
}
