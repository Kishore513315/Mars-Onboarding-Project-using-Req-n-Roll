Feature: Languages  
  As a registered user  
  I want to add, edit, delete and validate languages  
  So that I can manage my language proficiency successfully on my profile  

  Background:  
    Given I am logged into the Mars portal languages 
    And I navigate to the Languages tab  

  @AddLanguage  
  Scenario: TC-001 Add a new language successfully  
    When I click "Add New" button  
    And I enter language "English" with level "Fluent"  
    And I click "Add" button  
    Then I should see a languages message "English has been added to your languages"  

  @Negative  
  Scenario: TC-002 Try adding existing language again  
    When I click "Add New" button  
    And I enter language "English" with level "Basic"  
    And I click "Add" button  
    Then I should see a languages validation message "Duplicated data"

  @EditLanguage  
  Scenario: TC-003 Edit existing language to French  
    When I edit language "English" to "French"  
    Then I should see a languages message "French has been updated to your languages"  

  @AddSecondLanguage  
  Scenario: TC-004 Create new language English  
    When I click "Add New" button  
    And I enter language "English" with level "Basic"  
    And I click "Add" button  
    Then I should see a languages message "English has been added to your languages"  

  @DeleteLanguage  
  Scenario: TC-005 Delete French language  
    When I delete the language "French"  
    Then I should see a languages message "French has been deleted from your languages"  

  @Negative  
  Scenario: TC-008 Validate language text box by leaving it empty and click add  
    When I click "Add New" button  
    And I enter language "" with level "Conversational"  
    And I click "Add" button  
    Then I should see a languages validation message "Please enter language and level"  

  @Negative  
  Scenario: TC-017 Enter Numeric values in language  
    When I click "Add New" button  
    And I enter language "123456" with level "Conversational"  
    And I click "Add" button  
    Then I should see a languages validation message "Enter valid language"  

  @Negative  
  Scenario: TC-019 Enter Special Characters with language  
    When I click "Add New" button  
    And I enter language "Germ@n" with level "Basic"  
    And I click "Add" button  
    Then I should see a languages validation message "Enter valid language"  

    @DeleteLanguage  
  Scenario: TC-021 Delete Germ@n language  
    When I delete the language "Germ@n"  
    Then I should see a languages message "Germ@n has been deleted from your languages"

  @Negative  
  Scenario: TC-023 Enter Multiple languages in a new language using (,) and ( )  
    When I click "Add New" button  
    And I enter language "Telugu, Hindi, Marathi Polish" with level "Fluent"  
    And I click "Add" button  
    Then I should see a languages validation message "Enter single language only"  

  @Negative  
  Scenario: TC-025 Add new language using random characters, numbers, special characters and spaces  
    When I click "Add New" button  
    And I enter language "iaebfiulwejkdniwjklandbui3wkjqbdwihdjkbfoqjkad viehkrfb owjkadn ciwhkeab dcijknsd cxjk@4Y!IU#Y$E*(O1jk" with level "Basic"  
    And I click "Add" button  
    Then I should see a languages validation message "Enter valid language"  

  @Cleanup  
  Scenario: TC-027 Delete all languages after testing  
    When I delete all languages from the Languages tab  
    Then all languages should be removed successfully


