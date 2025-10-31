Feature: Skills
  As a registered user
  I want to add, edit, delete and validate skills
  So that I can manage my skill set successfully on my profile

  Background:
    Given I am logged into the Mars portal
    And I navigate to the Skills tab

  @AddSkill
  Scenario: TC-009 Add New Skill Successfully
    When I click on "Add New Skill" button
    And I enter skill name "Time management" with level "Intermediate"
    And I click on "Add" button
    Then I should see a message "Time management has been added to your skills"

  @EditSkill
  Scenario: TC-010 Edit an existing skill
    When I edit skill "Time management" to "Sales"
    Then I should see a message "Sales has been updated to your skills"

  @AddSecondSkill
  Scenario: TC-012 Add another skill
    When I click on "Add New Skill" button
    And I enter skill name "Time Management" with level "Expert"
    And I click on "Add" button
    Then I should see a message "Time Management has been added to your skills"

  @DeleteSkill
  Scenario: TC-015 Delete a skill
    When I delete the skill "Sales"
    Then I should see a message "Sales has been deleted"

