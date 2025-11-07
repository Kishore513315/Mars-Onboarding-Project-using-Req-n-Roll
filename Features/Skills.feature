Feature: Skills  
  As a registered user  
  I want to add, edit, delete and validate skills  
  So that I can manage my skill set successfully on my profile  

  Background:  
    Given I am logged into the Mars portal skills  
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


  @Negative  
  Scenario: TC-016 Validation Check — Leave both fields empty  
    When I click on "Add New Skill" button  
    And I enter skill name "" with level ""  
    And I click on "Add" button  
    Then I should see a validation message "Please enter skill and experience level"  

  @Negative  
  Scenario: TC-018 Enter Numeric values in Skills  
    When I click on "Add New Skill" button  
    And I enter skill name "123456" with level "Intermediate"  
    And I click on "Add" button  
    Then I should see a validation message "Enter valid skill"  

  @Negative  
  Scenario: TC-020 Enter Special Characters with Skill  
    When I click on "Add New Skill" button  
    And I enter skill name "J@v@" with level "Intermediate"  
    And I click on "Add" button  
    Then I should see a validation message "Enter valid skill"  

  @Negative  
  Scenario: TC-022 Enter Multiple Skills in a new Skill  
    When I click on "Add New Skill" button  
    And I enter skill name "Sports, Gym, Cycling C#" with level "Expert"  
    And I click on "Add" button  
    Then I should see a validation message "Enter single skill only"  

  @Negative  
  Scenario: TC-024 Add new Skill using random characters, numbers, special characters and spaces  
    When I click on "Add New Skill" button  
    And I enter skill name "iaebfiulwejkdniwjklandbui3wkjqbdwihdjkbfoqjkad viehkrfb owjkadn ciwhkeab dcijknsd cxjk@4Y!IU#Y$E*(O1jk" with level "Expert"  
    And I click on "Add" button  
    Then I should see a validation message "Enter valid skill"  

  @Cleanup  
  Scenario: TC-026 Delete all skills after testing  
    When I delete all skills from the Skills tab  
    Then all skills should be removed successfully


