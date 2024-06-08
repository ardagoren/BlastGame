# BlastGame
 
## Manager sınıfı Görevleri : 

- GridManager  
   - Her sahnedeki grid'i json verisine göre oluşturur.
   - Grid'deki objelerin aşağısında boşluk varsa vertical olarak düşmelerini sağlar.
- LevelManager
   - Hangi obstacle'dan kaç tane kaldığını ekranın sol üstünde sırasıyla gösterir.
   - Seviyenin tamamlanıp tamamlanmadığını kontrol eder.
- DataManager
   - Geçerli seviyeyi görüp değiştirmemizi sağlayan singleton yapısı.
   - Seviyeyi kaydeder ve kullanıcı çıkış yaptığında aynı yerden devam ettirir.
