using System;
using UTS_4;

void IntroGame()
{
    Console.Clear();
    Console.WriteLine("================================================");
    Console.WriteLine("Selamat Datang di Game RPG With Text in Terminal");
    Console.WriteLine("================================================");
}
// penentu berapa kali battle terjadi sebelum boss
IntroGame();
Random rnd = new Random();
int miniBattle = rnd.Next(3, 6); 
Statistik User = new Statistik();
// Biodata dan pemilihan role
Console.Write("Masukkan nama anda: ");
User.nama = Console.ReadLine();
Console.WriteLine("===============================");
Console.WriteLine("Pilih Role anda!");
Console.WriteLine("1.Novice");
Console.WriteLine("2.Rogue");
int pilihan = 0;
while (pilihan != 1 && pilihan != 2)
{
    Console.WriteLine("===============================");
    Console.Write(" (1 or 2) ");
    if (int.TryParse(Console.ReadLine(), out pilihan) && (pilihan == 1 || pilihan == 2))
    {
        Console.WriteLine("===============================");
        string Kelaspilihan = (pilihan == 1) ? "Novice" : "Rogue";
        Console.WriteLine($"{User.nama} adalah " + Kelaspilihan);
        Thread.Sleep(500);
        User.role = Kelaspilihan;
    }
    else{
        Console.WriteLine("Mohon pilih role terlebih dahulu");
        Thread.Sleep(1000);
    }
// Informasi statistik(Exp,HP,dan attack power)
}
Console.WriteLine("===============================");
User.Exp = 0;
User.AttackPower = 60;
User.HP = 120;

Statistik musuh1 = new Statistik();
musuh1.nama = "Pencuri";
musuh1.AttackPower = 15;
musuh1.HP = 40;
musuh1.Boss = false;

Statistik musuh2 = new Statistik();
musuh2.nama = "Maling";
musuh2.AttackPower = 25;
musuh2.HP = 50;
musuh2.Boss = false;

Statistik Boss = new Statistik();
Boss.nama = "Bos Penjahat";
Boss.AttackPower = 3 * User.AttackPower;
Boss.HP = 1000;
Boss.Boss = true;
Boss.SpecialBoss = 100000000;

// Case pertempuran
Console.WriteLine($"Selamatkan mereka dari para penjahat,{User.nama}!");
while (miniBattle > 0 && User.HP > 0)
{
    switch (miniBattle)
    {
        case 5:
            User.Attack(musuh1);
            miniBattle--;
            break;
        case 4:
            User.Attack(musuh2);
            miniBattle--;
            break;
        case 3:
            User.Attack(musuh1);
            miniBattle--;
            break;
        case 2:
            User.Attack(musuh2);
            miniBattle--;
            break;
        case 1:
            User.Attack(Boss);
            miniBattle--;
            break;
        default:
            Console.WriteLine("Tersesat");
            break;
    }
}

namespace UTS_4
{
    class Statistik
    {
        static Random random = new Random();
        static int level = 1;
        public int output = 1;
        public string? nama;
        public string? role;
        public int HP;
        public int AttackPower;
        public int SpecialBoss;
        public bool Boss;
        public int Exp;
        public int maxlevel = 20;
        int playerSkill = 2;
        static int blockedattack = 0;
        // Penyerangan
        public void Attack(Statistik target)
        {
            int tambahExp = random.Next(10, 40);

            while (target.HP > 0 && HP > 0)
            {
                Console.ReadKey();
                Console.Clear();
                Console.WriteLine("===============================");
                Console.WriteLine($"{target.nama} menyerang !");
                Console.WriteLine("===============================");
                Console.WriteLine($"Statistik {nama} : ");
                Console.WriteLine($"HP : {HP} || EXP : {Exp} || Class : {role}");
                Console.WriteLine($"Statistik {target.nama} : ");
                Console.WriteLine($"HP : {target.HP} || Attack : {target.AttackPower}");
                Console.WriteLine("===============================");
                Console.WriteLine("Apa yang akan kamu lakukan? ");
                Console.WriteLine(" 1) Cut Off");
                Console.WriteLine(" 2) Special Attack");
                Console.WriteLine(" 3) Blocking");
                Console.WriteLine(" 4) Run ");
                Console.WriteLine("===============================");
                try
                {
                    int playerPilihan = int.Parse(Console.ReadLine());
                    switch (playerPilihan)
                    {
                        case 1:
                            Exp += tambahExp;
                            SingleAttack(target);
                            EnemyAttack(target);
                            break;
                        case 2:
                            Exp += tambahExp;
                            SpecialAttack(target);
                            EnemyAttack(target);
                            break;
                        case 3:
                            Exp += tambahExp;
                            Defense(target);
                             Console.WriteLine("===============================");
                            Console.WriteLine($"Serangan {target.nama} terblokir !");
                            EnemyAttack(target);
                            blockedattack = -60;
                            break;
                        case 4:
                            target.HP = 0;
                             Console.WriteLine("===============================");
                            Console.WriteLine($"{nama} melarikan diri !");
                            break;
                        default:
                            Console.WriteLine("Input angka valid !");
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Pilihlah skill pada angka di atas!.");
                }
            }
            if (HP <= 0)
            {
                Console.WriteLine(" Dead !");
                Console.WriteLine(" Game Over !");
            }
            else
            {
                Console.WriteLine("Selamat "+nama+",anda menang!");
                Console.WriteLine(" Good Game !");
                LevelUp(Exp, maxlevel);
                PowerUp(maxlevel);
                Heal(target);
            }
            
        }

        public void Defense(Statistik target)
        {
            blockedattack += 60;
        }

        public void RagingBlow(Statistik target)
        {
            int Attacked = random.Next(3, 6);
            int totalDamage = 0;

            for (int i = 2; i < Attacked; i++)
            {
                int damage = AttackPower;
                target.HP -= damage;
                totalDamage += damage;
            }
     Console.WriteLine("===============================");
            Console.WriteLine($"Kamu menggunakan Raging Blow dan melakukan serangan {Attacked}x!");
            Console.WriteLine($"Total damage yang diterima adalah {totalDamage}.");
            EnemyAttack(target);
        }

        public void EnemyAttack(Statistik target)
        {
            Random rnd = new Random();
            if (Boss)
            {
                int i = rnd.Next(1, 101);

                if (i > 5)
                {
                    int damage = (int)(target.AttackPower + (rnd.Next(1, 3)) - blockedattack);
                    HP -= damage;
                    Console.WriteLine($"{target.nama} menyerangmu ! HP berkurang {damage}.");
                    
                }
                else
                {
                    int damage = target.SpecialBoss - blockedattack;
                    HP -= damage;
                    Console.WriteLine($"{target.nama} tak terkalahkan.");
                }
            }
            else{
                int damage = (int)(target.AttackPower + (rnd.Next(1, 3)) - blockedattack);
                HP -= damage;
                Console.WriteLine($"{target.nama} menusukmu ! HP berkurang {damage}.");
            }
        }

        private void SingleAttack(Statistik target)
        {
            int damage = AttackPower;
            target.HP -= damage;
             Console.WriteLine("===============================");
            Console.WriteLine($"Kamu melukai {target.nama} untuk {damage} damage.");
        }

        private void SpecialAttack(Statistik target)
        {
            if (role == "Novice" && playerSkill > 0)
            {
                playerSkill--;
                HP += 40 * level;
                 Console.WriteLine("===============================");
                Console.WriteLine($"Anda memakai Skill 'Istirahat'! HP sembuh {HP}.");
            }
            else if (role == "Rogue" && playerSkill > 0)
            {
                playerSkill--;
                int damage = random.Next(200, 300) * level;
               
        {
            int Attacked = random.Next(3, 6);
            int totalDamage = 0;

            for (int i = 2; i < Attacked; i++)
            {
                target.HP -= damage;
                totalDamage += damage;
            }
     Console.WriteLine("===============================");
            Console.WriteLine($"Kamu menggunakan Raging Blow dan melakukan serangan {Attacked}x !");
            Console.WriteLine($"Total kerusakan yang diakibatkan adalah {totalDamage}.");
            EnemyAttack(target);
        }         
            }
            else{
                Console.WriteLine("Anda sudah memakai semua skill.");
            }
        }

        public int LevelUp(int Exp, int maxlevel)
        {
            if (Exp >= maxlevel)
            {
                maxlevel += 15;
                Exp = 0;
                output++;
            }
            return output;
        }

        public void PowerUp(int maxlevel)
        {
            int firstLevel = 1;
            level = LevelUp(Exp, maxlevel);
            if (level > firstLevel)
            {
                AttackPower *= 3;
                playerSkill += 6;
                firstLevel++;
                Console.WriteLine($"Anda level : {level}.");
                HP += 200;
                maxlevel = 0;
            }
        }
        private void Heal(Statistik target)
        {
            target.HP += 50;
        }
    }
}