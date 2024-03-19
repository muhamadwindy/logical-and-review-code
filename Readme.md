# A.	Logical and Review Code 
_Oleh : Muhamad Windy Sulistiyo_
## 1.	How about your opinion..? 

        if (application != null)
        {
            if (application.protected != null)
            {
                return application.protected.shieldLastRun;
            }
        }

__Key: cleaner and easier to read code.__

Saya bisa membuat kode tersebut menjadi lebih simple dan mudah dibaca dengan memanfaatkan operator null-conditional(?) dan null coalescing(??) menjadi seperti berikut : 

    result = application?.Protected?.shieldLastRun ??   DateTime.Now;

Dalam kode di atas, operator null conditional digunakan untuk melakukan pengecekan null dan operator ini bisa menghentikan pengecekan jika nilai sebelumnya adalah null, sehingga tidak perlu menulis blok if untuk setiap tingkat properti. jadi jika application memiliki nilai null dia akan berhenti dan tidak melanjutkan ke pengecekan protected. begitu pula protected jika dia memiliki nilai null maka tidak melanjutkan ke shieldLastRun.

kemudian untuk shieldLastRun saya menggunakan operator null-coalesce yang bisa memberikan nilai default jika variabel tersebut bernilai null.


## 2. How about your opinion.
    public ApplicationInfo GetInfo()
    {
        var application = new ApplicationInfo
        {
            Path = "C:/apps/",
            Name = "Shield.exe"
        };
        return application;
    }
__Key: return more than one value from a class method.__


Untuk memperbarui fungsi GetInfo() sehingga mengembalikan lebih dari satu nilai, saya menggunakan tuple atau membuat kelas baru untuk menyimpan beberapa nilai yang ingin dikembalikan. Mungkin ini bukan cara yang terbaik jika saya memiliki program yang kompleks, akan tetapi ini merupakan cara menjadikan simple ketika kasusnya seperti soal tersebut.


## 3. How about your opinion.
    class Laptop
    {
        public string Os{ get; set; } // can be modified
        public Laptop(string os)
        {
            Os= os;
        }
    }
    var laptop = new Laptop("macOs");
    Console.WriteLine(Laptop.Os); // Laptop os: macOs

__Key: modifications by using private members.__

 Dalam kode tersebut, variable Os saya ubah menjadi private _os. Kemudian saya membuat fungsi GetOs() yang bisa digunakan untuk mengakses nilai _os yang dimana hal ini mengubah yang tadinya bisa mengakses langsung variabel Os, saat ini hanya kelas Laptop yang dapat mengakses nilai dari _os dan jika ingin mencetak nilai Os, kita buatkan fungsi yang bernama GetOs().



## 4. How about your opinion?

    using System;
    using System.Collections.Generic;
    namespace MemoryLeakExample {
    class Program {
        static void Main(string[] args) {
        var myList = new List();
        while (true) {
            // populate list with 1000 integers 
            for (int i = 0; i < 1000; i++) {
            myList.Add(new Product(Guid.NewGuid().ToString(), i));
            }
            // do something with the list object 
            Console.WriteLine(myList.Count);
        }
        }
    }
    class Product {
        public Product(string sku, decimal price) {
        SKU = sku;
        Price = price;
        }
        public string SKU {
        get;
        set;
        }
        public decimal Price {
        get;
        set;
        }
    }
    }

__Key:
Keeping references to objects unnecessarily__

Memori Leak adalah kondisi dimana ketika kita membuat atau mengalokasikan memori di list atau variabel penyimpan apapun dan tidak menghapusnya sehingga menyebabkan memori yang tersedia di ram menjadi berkurang atau leak. dikarenakan terlalu banyak memori yang tersedia dapat dialokasikan hal itu menyebabkan sistem menjadi lambat, stuck dan bahkan tidak bisa bekerja sama sekali.
 
 Solusinya adalah ketika selesai mencetak jumlah 1.000 bilangan dari variable myLis, maka saya melakukan clear untuk mengosongkan List. dengan demikian memori dari myList dikosongkan.

## 5. How about your opinion?

    using System;
    namespace MemoryLeakExample {
    class Program {
        static void Main(string[] args) {
        var publisher = new EventPublisher();
        while (true) {
            var subscriber = new EventSubscriber(publisher);
            // do something with the publisher and subscriber objects 
        }
        }
        class EventPublisher {
        public event EventHandler MyEvent;
        public void RaiseEvent() {
            MyEvent?.Invoke(this, EventArgs.Empty);
        }
        }
        class EventSubscriber {
        public EventSubscriber(EventPublisher publisher) {
            publisher.MyEvent += OnMyEvent;
        }
        private void OnMyEvent(object sender, EventArgs e) {
            Console.WriteLine("MyEvent raised");
        }
        }
    }
    }

__Key: event handlers__

Secara konsep pengalokasian memory, hal ini juga terkait dengan nomor 4 yang dimana penggunaan event handler yang kurang tepat juga bisa menyebabkan memory menjadi leak. 

Solusi untuk class EventSubscriber saya refaktor menjadi kelas EventSubscriberAnswer. Kelas baru tersebut saya buatkan method dispose() untuk membuang event yang sudah tidak terpakai lagi dengan validasi yang memanfaatkan variabel isDisposed jika sudah dibersihkan maka tidak akan dibersihkan lagi namun jika belum dibersihkan maka dibersihkan dengan menggunakan operator unary(-=) sepreti di method.
 Kemudian saya juga memanggil GC.SuppressFinalize(this);. 
 
 Fungsi itu saya gunakan untuk membersihkan sumber daya memory lain yang tidak terkelola sebelum objek dihapus dari memori. menurut literatur yang saya baca dan saya gunakan di proyek sebelumnya fungsi itu saya gunakan di dalam method dispose();


## 6. How about your opinion?

    using System;
    using System.Collections.Generic;
    namespace MemoryLeakExample {
        class Program {
            static void Main(string[] args) {
            var rootNode = new TreeNode();
            while (true) { // create a new subtree of 10000 nodes 
                var newNode = new TreeNode();
                for (int i = 0; i < 10000; i++) {
                var childNode = new TreeNode();
                newNode.AddChild(childNode);
                }
                rootNode.AddChild(newNode);
            }
            }
        }
        class TreeNode {
            private readonly List < TreeNode > _children = new List < TreeNode > ();
            public void AddChild(TreeNode child) {
            _children.Add(child);
            }
        }
    }

__Key:Large object graphs__

Meskipun menggunakan nested data, hal itu secara konsep pengosongan memory sama dengan nomor 4, akan tetapi secara teknis hal ini sedikit tricky ketika saya perlu mengecek jumlah child dengan menggunakan method getChildrenLength() untuk mengetahui jumlah childnya yang kemudian ketika jumlahnya lebih dari 10 maka saya hapus data pada index pertama dengan method yang saya buat yaitu RemoveFirstChild() sehingga jumlah child akan selalu 10.

## 7. How about your opinion?

    using System;
    using System.Collections.Generic;
    class Cache {
        private static Dictionary < int, object > _cache = new Dictionary < int, object > ();

        public static void Add(int key, object value) {
            _cache.Add(key, value);
        }

        public static object Get(int key) {
            return _cache[key];
        }

        }
        class Program {
        static void Main(string[] args) {
            for (int i = 0; i < 1000000; i++) {
            Cache.Add(i, new object());
            }
            Console.WriteLine("Cache populated");
            Console.ReadLine();
        }
    }

__Key:
Improper caching__

Konsep Cache secara umum adalah suatu hal yang digunakan untuk menyimpan data yang telah diakses sebelumnya sehingga data tersebut dapat diakses lebih cepat pada akses berikutnya dengan tujuan mengurangi waktu pengaksesan data dari sumber aslinya. ketika cache diimplementasikan tanpa memperhatikan kapan cache tersebut kadaluarsa, maka menjadikan memory menjadi leak. Kode tersebut saya refaktor menjadi class CacheAnswer.

Untuk memudahkan menyimpan kapan cache kadaluarsa, saya membuat kelas baru CacheItem. Kemudian struktur dictionary cache saya ubah menjadi <int, CacheItem>. Selanjutnya method Add saya tambah parameter DateTime expired yang kemudian saya masukan ke objek dari CacheItem termasuk dengan value nya kemudian parameter kedua dari Add saya ganti menjadi objek dari CacheItem.


## 8. Web Application Development

Aplikasi ini dibuat untuk Business Unit untuk   menghandle dokumen (xlsx dan pdf) dari pelanggan 
dan menyimpan dokumen tersebut ke dalam database. 

Sebelum mengirimkan dokumen yang dibutuhkan, 
customer harus melakukan registrasi jika belum terdaftar. 
Sistem harus mengotentikasi user sebagai pelanggan sebelum mengirim dokumen ke sistem. 
Password yang digunakan harus memenuhi persyaratan seperti yang tertera di bawah ini:

- Terdiri dari setidaknya satu karakter bahasa Inggris huruf kecil.
- Mengandung setidaknya satu karakter bahasa Inggris huruf besar.
- Mengandung setidaknya satu karakter khusus. Karakter khusus tersebut adalah: !@#$%^&*()-+
- Panjangnya minimal 8.
- Berisi setidaknya satu digit.

Ketika dokumen berhasil tersimpan di database (transaksi selesai), 
sistem akan mengirimkan notifikasi kepada pelanggan sebagai tanda terima bahwa dokumen berhasil dikirimkan. 

Ukuran dokumen lebih dari 1 atau 2 GB, dan secara teknis memerlukan penanganan seperti metode chunking.
Unit bisnis dapat memonitor dokumen yang dikirimkan oleh pelanggan dan 
unit bisnis dapat mengunduh dokumen tersebut.

### Spesifikasi Aplikasi 
Aplikasi ini saya buat dengan arsitektur seperti berikut :
    
1.  __Backend__ 

    Web API ASP.NET Core menggunakan framework .NET 7,
    Entity Framework,
    ASP.NET Core Identity
    
2.  __Frontend__

    ReactJS dengan setup menggunakan vite.
    Axios,
    React Router,
    TailwindCSS,
    Ripple UI,
    React Hook Form,
    Redux,
    Redux Toolkit,
    SweetAlert 2

3.  __Database__

    Microsoft SQL Server
    