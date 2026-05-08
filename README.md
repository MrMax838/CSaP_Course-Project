# Привітики

Тут буде опис курсового проєкту

# Засоби для автентифікації даних за допомогою алгоритму SHA-3

Що це?
Навіщо?
Яку проблему вирішує?

## Пояснення до теми

> **Автентифікація даних**  
передбачає реалізацію криптографічних методів перевірки цілісності та автентичності даних, які базуються на методах формування електронних цифрових підписів та гешування даних. Завдання такого типу потребують аналізу студентом відомих методів формування електронних цифрових підписів або методів гешування, визначення переваг і недоліків цих методів порівняно з відомими аналогами, а також визначення задач, в яких доцільно використовувати пристрій, що розробляється. При цьому основна увага має приділятися генеруванню відкритого та закритого ключів для електронних цифрових підписів, реалізації алгоритмів формування цифрових підписів або геш-значень даних, організації віддаленого обміну даними між користувачами відповідно до цього криптографічного протоколу. 



# Структура (видалити перед завершенням проєкту)
// # Project Name

// Short description.

// ## Features

// ## Installation

// ## Usage

// ## Configuration

// ## Project Structure

// ## Contributing

// ## License 

RSA.cs  
|  
├──RSACore    
|  ├──RSAModules  
|  |  ├──ModularArithmetic.cs  
|  |  └──PrimeGenerator.cs  
|  └──RSACore.cs  
|  
├──RSAKeys  
|  ├──IKey.cs  
|  ├──RSAKeyGenerator.cs  
|  └──RSAKeyPair.cs  
├──RSAPackaging  
|  ├──OAEP  
|  |  ├──DBGenerator.cs  
|  |  ├──OAEP.cs  
|  |  └──SeedGenerator.cs  
|  ├──PSS  
|  |  ├──DBGenerator.cs  
|  |  ├──PSS.cs  
|  |  └──SaltGenerator.cs  
|  ├──MaskGeneration.cs  
|  └──TypesFormatter.cs  
└──Wrapper  
   └──Wrapper.cs  

DataAuthenticationUsingSHA-3  
|  
├──Initialization  
|  ├──ApplicationContext.cs  
|  └──Initializer.cs  
|  
├──IOData  
|  ├──Messages.json  
|  └──Users.json  
|  
├──MessageModel  
|  ├──MessageRecord.cs  
|  ├──MessageRepository.cs  
|  └──SignedMessage.cs  
|  
├──Shell  
|  ├──Commands  
|  |  ├──DeletaMessageCommand.cs  
|  |  ├──ExiteCommand.cs  
|  |  ├──HelpCommand.cs  
|  |  ├──ICommand.cs  
|  |  ├──ListCommand.cs  
|  |  ├──SendCommand.cs  
|  |  ├──TamperCommand.cs  
|  |  └──VerifyCommand.cs  
|  |  
|  └──Parser.cs  
|  
├──UserModel  
|  ├──IUser.cs  
|  ├──User.cs  
|  ├──UserRecord.cs  
|  └──UserRepository.cs  
|  
├──RSA  
|  ├──RSACore    
|  |  ├──RSAModules    
|  |  |  ├──ModularArithmetic.cs    
|  |  |  └──PrimeGenerator.cs  
|  |  └──RSACore.cs  
|  |  
|  ├──RSAKeys  
|  |  ├──IKey.cs  
|  |  ├──RSAKeyGenerator.cs  
|  |  └──RSAKeyPair.cs  
|  |  
|  ├──RSAPackaging  
|  |  ├──OAEP  
|  |  |  ├──DBGenerator.cs  
|  |  |  ├──OAEP.cs  
|  |  |  └──SeedGenerator.cs  
|  |  |  
|  |  ├──PSS  
|  |  |  ├──DBGenerator.cs  
|  |  |  ├──PSS.cs  
|  |  |  └──SaltGenerator.cs  
|  |  |  
|  |  ├──MaskGeneration.cs  
|  |  └──TypesFormatter.cs  
|  |  
|  ├──Wrapper    
|  |  └──Wrapper.cs  
|  |  
|  └──RSA.cs  
|  
├──Program.cs  
└──README.md  


Available commands:

help
    Show available commands

list users
    Show all users

list messages
    Show all messages

send
    Create and sign message

verify <messageID>
    Verify message signature

verify <messageID> --verbose
    Show detailed verification process

tamper <messageID>
    Simulate message tampering attack

delete <messageID>
    Delete message

exit
    Exit application


message modified
signature old

20. Але:

пізніше можна додати:

send --auto-id