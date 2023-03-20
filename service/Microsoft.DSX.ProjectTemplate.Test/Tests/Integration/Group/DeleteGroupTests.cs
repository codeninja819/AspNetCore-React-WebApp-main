﻿using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Microsoft.DSX.ProjectTemplate.Test.Tests.Integration.Group
{
    [TestClass]
    [TestCategory("Groups - Delete")]
    public class DeleteGroupTests : BaseGroupTest
    {
        [DataTestMethod]
        [DataRow(5)]
        public async Task DeleteGroup_ValidId_Success(int groupId)
        {
            // Arrange

            // Act
            using var response = await Client.DeleteAsync($"/api/groups/{groupId}");

            // Assert
            var result = await EnsureObject<bool>(response);
            result.Should().BeTrue();
        }

        [DataTestMethod]
        [DataRow(0)]
        [DataRow(-1)]
        public async Task DeleteGroup_InvalidId_BadRequest(int groupId)
        {
            // Arrange

            // Act
            using var response = await Client.DeleteAsync($"/api/groups/{groupId}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [DataTestMethod]
        [DataRow(int.MaxValue)]
        public async Task DeleteGroup_IdDoesNotExist_NotFound(int groupId)
        {
            // Arrange

            // Act
            // FIXME: hitting an unknown content stream exception, using workaround - https://stackoverflow.com/a/65532357/1448448
            //var response = await Client.DeleteAsync($"/api/groups/{groupId}");
            using var response = await Client.SendAsync(new HttpRequestMessage(HttpMethod.Delete, $"/api/groups/{groupId}"), HttpCompletionOption.ResponseHeadersRead);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
