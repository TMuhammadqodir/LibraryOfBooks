Assalomu alekum mening ismim Turidyev Muhammadqodir. Bu loyiha online kutubxona shaklida unda foydalanuvchilar kirib kitoblarini qo'shishi boshqalarning qo'shgan kitoblarini ko'rishlari mumkin

Dasturni run qilishdan oldin bir nechta qadamlar 
1. Migrasiya qilingan malumotlar bazsini yangilab olish  Tools -> NuGet Package Manager -> Package Manager Console da ushbu buyruqni kiritamiz "update-database" malumotlar bazasi tayyor holatga keladi.
2. Solution exploreda Solution ustida o'ng tugma bosiladi Configure startup porjects ga kirib Multiple startup projects ni tanlemiz va "LibraryOfBooks.Web" va "LibraryOfBooks.WebApi" ni start holatiga qo'yamiz
3. Docker-compose linux uchun sozlangan agar sizda windows bo'lsa xatolik bo'lsihi mumkin. Bu holatda ishlatishni osonroq yo'li appsettings.json faylida DB configurasiyadagi hostni "localhost" ga o'zgartirish

Dastur run bo'lgandan so'ng
1. Dastur run qilinadi agar blazorda yani UI da run bo'lganda qandaydir xato bo'lgan bo'lsa Api sal kechroq ishlagan bo'lishi mumkin sahifani bitta yangilab yuboramiz
2. Agar xatolik tuzalmagan bo'lsa "LibraryOfBooks.Web" va "LibraryOfBooks.WebApi" ning program.cs qismida bir birni manzili to'g'ri ko'rsatilganlini tekshirib ko'ramiz.
   
