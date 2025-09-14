using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoTask.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRelationIssueConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_relation_issues_issues_RelatedIssueId",
                table: "relation_issues");

            migrationBuilder.DropIndex(
                name: "IX_relation_issues_RelatedIssueId",
                table: "relation_issues");

            migrationBuilder.DropColumn(
                name: "RelatedIssueId",
                table: "relation_issues");

            migrationBuilder.CreateIndex(
                name: "IX_relation_issues_related_id",
                table: "relation_issues",
                column: "related_id");

            migrationBuilder.AddForeignKey(
                name: "fk_relation_issue_related_issue",
                table: "relation_issues",
                column: "related_id",
                principalTable: "issues",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_relation_issue_related_issue",
                table: "relation_issues");

            migrationBuilder.DropIndex(
                name: "IX_relation_issues_related_id",
                table: "relation_issues");

            migrationBuilder.AddColumn<Guid>(
                name: "RelatedIssueId",
                table: "relation_issues",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_relation_issues_RelatedIssueId",
                table: "relation_issues",
                column: "RelatedIssueId");

            migrationBuilder.AddForeignKey(
                name: "FK_relation_issues_issues_RelatedIssueId",
                table: "relation_issues",
                column: "RelatedIssueId",
                principalTable: "issues",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
