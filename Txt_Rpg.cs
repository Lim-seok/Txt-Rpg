using System; // 기본 C# 기능을 사용하기 위한 네임스페이스
using System.Collections.Generic; // List<T>와 같은 제네릭 컬렉션을 사용하기 위한 네임스페이스
using System.Linq; // LINQ 쿼리 기능을 사용하기 위한 네임스페이스

// 아이템 타입을 정의하는 열거형
public enum ItemType // enum: 관련된 상수들의 집합을 정의하는 자료형
{
    Weapon,    // 무기 타입을 나타내는 상수
    Armor,     // 방어구 타입을 나타내는 상수
    Consumable // 소비 아이템 타입을 나타내는 상수
}


// 아이템 클래스: 게임 내 아이템의 속성을 정의
public class Item
{
    public string Name { get; set; }        // 아이템 이름
    public ItemType Type { get; set; }      // 아이템 타입
    public int AttackBonus { get; set; }    // 공격력 보너스
    public int DefenseBonus { get; set; }   // 방어력 보너스
    public string Description { get; set; } // 아이템 설명
    public int Price { get; set; }          // 아이템 가격
    public bool IsEquipped { get; set; }    // 장착 여부

    // 아이템 생성자
    public Item(string name, ItemType type, int attackBonus, int defenseBonus, int price, string description)
    {
        Name = name;
        Type = type;
        AttackBonus = attackBonus;
        DefenseBonus = defenseBonus;
        Price = price;
        Description = description;
        IsEquipped = false;
    }
}


// 아이템 관리자 클래스: 모든 아이템 정보를 관리
public class ItemManager
{
    // List<Item> 자료형: Item 객체들을 저장하는 리스트
    // allItems 변수: 게임의 모든 아이템 정보를 저장하는 리스트
    private List<Item> allItems;

    // 생성자: ItemManager 객체를 초기화하고 기본 아이템들을 생성
    public ItemManager()
    {
        // new List<Item>: 새로운 Item 리스트를 생성
        // allItems에 기본 아이템들을 추가
        allItems = new List<Item>
        {
            // new Item(): 각 아이템 객체를 생성
            // 매개변수: (이름, 타입, 공격력, 방어력, 가격, 설명)
            new Item("낡은 검", ItemType.Weapon, 2, 0, 600, "쉽게 볼 수 있는 낡은 검입니다."),
            new Item("청동 도끼", ItemType.Weapon, 5, 0, 1500, "어디선가 사용됐던 것 같은 도끼입니다."),
            new Item("스파르타의 창", ItemType.Weapon, 7, 0, 2000, "스파르타의 전사들이 사용했다는 전설의 창입니다."),
            new Item("수련자 갑옷", ItemType.Armor, 0, 5, 1000, "수련에 도움을 주는 갑옷입니다."),
            new Item("무쇠갑옷", ItemType.Armor, 0, 9, 2000, "무쇠로 만들어져 튼튼한 갑옷입니다."),
            new Item("스파르타의 갑옷", ItemType.Armor, 0, 15, 3500, "스파르타의 전사들이 사용했다는 전설의 갑옷입니다."),
            new Item("체력 포션", ItemType.Consumable, 0, 0, 500, "체력을 회복시켜주는 물약입니다."),
            new Item("야만의 몽둥이", ItemType.Weapon, 25, 0, 1337, "전설의 야만의 몽둥이입니다.")
        };
    }

    // 메서드: 모든 아이템 반환
    // 반환 자료형: List<Item>
    // 목적: 저장된 모든 아이템의 리스트를 반환
    public List<Item> GetAllItems()
    {
        return allItems;
    }

    // 메서드: 특정 타입의 아이템만 반환
    // 매개변수: ItemType type - 찾고자 하는 아이템의 타입
    // 반환 자료형: List<Item>
    // 목적: 주어진 타입에 해당하는 아이템들만 필터링하여 리스트로 반환
    public List<Item> GetItemsByType(ItemType type)
    {
        // LINQ Where 메서드: 조건에 맞는 요소만 선택
        // ToList(): 결과를 새로운 리스트로 변환
        return allItems.Where(item => item.Type == type).ToList();
    }

    // 메서드: 이름으로 아이템 찾기
    // 매개변수: string name - 찾고자 하는 아이템의 이름
    // 반환 자료형: Item
    // 목적: 주어진 이름과 일치하는 아이템을 찾아 반환
    public Item GetItemByName(string name)
    {
        // LINQ Find 메서드: 조건에 맞는 첫 번째 요소를 반환
        return allItems.Find(item => item.Name == name);
    }
}

// 플레이어 클래스: 게임 플레이어의 속성과 행동을 정의
public class Player
{
    public string Name { get; set; }
    public string Job { get; set; }
    public int Level { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }
    public int Health { get; set; }
    public int Gold { get; set; }
    public List<Item> Inventory { get; set; }

    public Player()
    {
        Inventory = new List<Item>();
    }

    public void DisplayStatus()
    {
        Console.WriteLine("상태 보기");
        Console.WriteLine("캐릭터의 정보가 표시됩니다.");
        Console.WriteLine();
        Console.WriteLine($"Lv. {Level:D2}");
        Console.WriteLine($"{Name} ( {Job} )");
        Console.WriteLine($"공격력 : {Attack + GetTotalAttackBonus()} (+{GetTotalAttackBonus()})");
        Console.WriteLine($"방어력 : {Defense + GetTotalDefenseBonus()} (+{GetTotalDefenseBonus()})");
        Console.WriteLine($"체 력 : {Health}");
        Console.WriteLine($"Gold : {Gold} G");
        Console.WriteLine("\n아무 키나 누르면 메인 메뉴로 돌아갑니다...");
        Console.ReadKey(); // 사용자 입력 대기
    }

    private int GetTotalAttackBonus()
    {
        return Inventory.Where(item => item.IsEquipped).Sum(item => item.AttackBonus);
    }

    private int GetTotalDefenseBonus()
    {
        return Inventory.Where(item => item.IsEquipped).Sum(item => item.DefenseBonus);
    }

    public void ToggleEquipItem(Item item)
    {
        item.IsEquipped = !item.IsEquipped;
    }
}

// 게임 클래스: 전체 게임 로직을 관리
public class Game
{
    // Player 자료형: 플레이어 정보를 저장하는 객체
    // player 변수: 현재 게임의 플레이어 정보를 저장
    private Player player;

    // ItemManager 자료형: 게임 내 모든 아이템을 관리하는 객체
    // itemManager 변수: 아이템 관리자 인스턴스를 저장
    private ItemManager itemManager;

    // 생성자: Game 객체를 초기화하고 기본 플레이어와 아이템 매니저를 생성
    public Game()
    {
        // 새로운 Player 객체 생성 및 초기화
        player = new Player
        {
            Name = "Chad",
            Job = "전사",
            Level = 1,
            Attack = 10,
            Defense = 5,
            Health = 100,
            Gold = 1500
        };

        // ItemManager 객체 생성
        itemManager = new ItemManager();
    }

    // 메서드: 게임 시작 및 메인 루프
    // 반환 자료형: void
    // 목적: 게임의 메인 메뉴를 표시하고 사용자 입력에 따라 적절한 동작 수행
    public void Start()
    {
        while (true)
        {
            Console.Clear(); // 화면 초기화
            Console.WriteLine("스파르타 마을에 오신 용사님, 환영합니다.");
            Console.WriteLine("\n무엇을 하시겠습니까?");
            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 상점");
            Console.WriteLine("4. 휴식하기");
            Console.WriteLine("0. 종료");

            string input = Console.ReadLine(); // 사용자 입력 받기
            Console.Clear(); // 선택 후 화면 초기화
            switch (input)
            {
                case "1":
                    player.DisplayStatus();
                    break;
                case "2":
                    ManageInventory();
                    break;
                case "3":
                    EnterShop();
                    break;
                case "4":
                    GetRest();
                    break;
                case "0":
                    Console.WriteLine("게임을 종료합니다.");
                    return;
                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    break;
            }
        }
    }

    // 메서드: 인벤토리 관리
    // 반환 자료형: void
    // 목적: 플레이어의 인벤토리를 표시하고 관리하는 기능 제공
    private void ManageInventory()
    {
        while (true)
        {
            Console.Clear(); // 화면 초기화
            Console.WriteLine("\n인벤토리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine("\n[아이템 목록]");

            if (player.Inventory.Count == 0)
            {
                Console.WriteLine("보유 중인 아이템이 없습니다.");
            }
            else
            {
                // 플레이어의 인벤토리 아이템 목록 표시
                for (int i = 0; i < player.Inventory.Count; i++)
                {
                    Item item = player.Inventory[i];
                    string equippedMark = item.IsEquipped ? "[E]" : "   ";
                    Console.WriteLine($"{i + 1}. {equippedMark}{item.Name} | 공격력 +{item.AttackBonus} | 방어력 +{item.DefenseBonus} | {item.Description}");
                }
            }

            Console.WriteLine("\n1. 장착 관리");
            Console.WriteLine("0. 나가기");

            Console.Write("\n원하시는 행동을 입력해주세요.\n>> ");
            string input = Console.ReadLine();

            if (input == "0") break;
            if (input == "1") ManageEquipment();
        }
    }

    // 메서드: 장비 관리
    // 반환 자료형: void
    // 목적: 플레이어의 장비 장착 상태를 관리하는 기능 제공
    private void ManageEquipment()
    {
        while (true)
        {
            Console.Clear(); // 화면 초기화
            Console.WriteLine("\n인벤토리 - 장착 관리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine("\n[아이템 목록]");

            // 플레이어의 인벤토리 아이템 목록 표시
            for (int i = 0; i < player.Inventory.Count; i++)
            {
                Item item = player.Inventory[i];
                string equippedMark = item.IsEquipped ? "[E]" : "   ";
                Console.WriteLine($"{i + 1}. {equippedMark}{item.Name} | 공격력 +{item.AttackBonus} | 방어력 +{item.DefenseBonus} | {item.Description}");
            }

            Console.WriteLine("\n0. 나가기");

            Console.Write("\n원하시는 행동을 입력해주세요.\n>> ");
            string input = Console.ReadLine();

            if (input == "0") break;

            // 사용자 입력을 정수로 변환하고 유효성 검사
            if (int.TryParse(input, out int index) && index > 0 && index <= player.Inventory.Count)
            {
                Item selectedItem = player.Inventory[index - 1];
                player.ToggleEquipItem(selectedItem);
                Console.WriteLine(selectedItem.IsEquipped ? $"{selectedItem.Name}을(를) 장착했습니다." : $"{selectedItem.Name}을(를) 해제했습니다.");
                Console.ReadKey(); // 사용자가 결과를 확인할 수 있도록 잠시 대기
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
                Console.ReadKey(); // 사용자가 오류 메시지를 확인할 수 있도록 잠시 대기
            }
        }
    }

    // 메서드: 상점 입장
    // 반환 자료형: void
    // 목적: 상점 메뉴를 표시하고 아이템 구매 기능 제공
    private void EnterShop()
    {
        while (true)
        {
            Console.Clear(); // 화면 초기화
            Console.WriteLine("\n상점");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine($"\n[보유 골드]\n{player.Gold} G");
            Console.WriteLine("\n[아이템 목록]");

            List<Item> shopItems = itemManager.GetAllItems();
            // 상점의 모든 아이템 목록 표시
            for (int i = 0; i < shopItems.Count; i++)
            {
                Item item = shopItems[i];
                string purchaseStatus = player.Inventory.Any(i => i.Name == item.Name) ? "구매완료" : $"{item.Price} G";
                Console.WriteLine($"{i + 1}. {item.Name} | 공격력 +{item.AttackBonus} | 방어력 +{item.DefenseBonus} | {item.Description} | {purchaseStatus}");
            }

            Console.WriteLine("\n1. 아이템 구매");
            Console.WriteLine("0. 나가기");

            Console.Write("\n원하시는 행동을 입력해주세요.\n>> ");
            string input = Console.ReadLine();

            if (input == "0") break;
            if (input == "1") PurchaseItem();
        }
    }

    private void GetRest()
    {
        Console.Clear();
        Console.WriteLine("휴식하기");
        Console.WriteLine("휴식하는데 필요한 골드는 500G입니다.");
        Console.WriteLine($"\n[보유골드]\n{player.Gold}G");

        Console.WriteLine("1. 휴식하기");
        Console.WriteLine("0. 나가기");

        Console.Write("\n원하시는 행동을 입력해주세요.\n>> ");
        string input = Console.ReadLine();

        if (input == "0") return;
        if (input == "1")
        {
            Rest();
        }
    }
    private void Rest()
    {
        int restcost = 500;
        if(player.Gold > restcost )
        {
            Console.Clear();
            player.Gold -= restcost;
            player.Health += 100;
            Console.WriteLine("휴식을 완료했습니다.");
            Console.WriteLine("체력을 100 회복합니다.");

        }
        else
        {
            Console.WriteLine("Gold가 부족하다.");
        }
        Console.ReadKey();


    }

    // 메서드: 아이템 구매
    // 반환 자료형: void
    // 목적: 플레이어가 선택한 아이템을 구매하는 기능 제공
    private void PurchaseItem()
    {
        Console.Write("\n구매할 아이템의 번호를 입력하세요: ");
        // 사용자 입력을 정수로 변환하고 유효성 검사
        if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= itemManager.GetAllItems().Count)
        {
            Item selectedItem = itemManager.GetAllItems()[index - 1];

            if (player.Inventory.Any(i => i.Name == selectedItem.Name))
            {
                Console.WriteLine("이미 구매한 아이템입니다.");
            }
            else if (player.Gold >= selectedItem.Price)
            {
                player.Gold -= selectedItem.Price;
                player.Inventory.Add(new Item(selectedItem.Name, selectedItem.Type, selectedItem.AttackBonus, selectedItem.DefenseBonus, selectedItem.Price, selectedItem.Description));
                Console.WriteLine("구매를 완료했습니다.");
            }
            else
            {
                Console.WriteLine("Gold가 부족합니다.");
            }
        }
        else
        {
            Console.WriteLine("잘못된 입력입니다.");
        }
        Console.ReadKey(); // 사용자가 결과를 확인할 수 있도록 잠시 대기
    }
}

class Program
{
    static void Main(string[] args)
    {
        Game game = new Game();
        game.Start();
    }
}