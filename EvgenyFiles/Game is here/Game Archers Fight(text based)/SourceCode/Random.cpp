#include<iostream>
#include<fstream>
#include<cstdlib>
#include<random>
#include<string>
#include<sstream>
#include"irrKlang.h"

using namespace std;
using namespace irrklang;

#pragma comment(lib,"irrKlang")


// 85 atack xbow vs 50% helmet |   1 shoted distance <20         90hp distance <35     80hp distance <50   damage: 90-110 %     
//melee damage 

//range weapon slot1 ammo slot 2 mille weapon slot 3 shield slot4
int getseed()
{
	cout<<"Outside source is connected"<<endl;
	int seed;
	// OPEN FILE TO READ
	ifstream infile;
	infile.open("Random.txt");
	if(infile.fail())
	{
		cout << "Error opening file"<<endl;
	}
	else
	{
		infile>>seed;
	}
	infile.close();
	
	// OPEN FILE TO WRITE
	ofstream onfile;
	onfile.open("Random.txt");
	if(onfile.fail())
	{
		cout << "Error opening file"<<endl;
	}
	else
	{
		seed=seed+1;
		onfile.clear();
		onfile<<seed;
		cout<<seed<<endl;
	}
	onfile.close();
	return seed;
}
int dist_min=0;
int dist_max=50;
ISoundEngine* soundEngine = createIrrKlangDevice();

class weapon
{
public:
	string name;
	double damage;
	int armor_penetration;
	bool range;
	bool ammo;
	int cost;
	
	// functions for creating object
	weapon givebackfromarray(weapon arr[],int size,string name);
	void weapon_info();
	weapon();
	weapon(string name_,double damage_,bool ammo_,bool range_,int cost_, int armor_penetration_);
};
weapon::weapon()
{
	name="NULL";
	damage=0.00;
	armor_penetration=0;
	ammo=false;
	range=false;
	cost=0;

}
void weapon::weapon_info()
{
	cout<<"Name: "<<name<<endl;
	cout<<"Cost: "<<cost<<endl;
	cout<<"Damage: "<<damage<<endl;
	cout<<"Ignoring armor %: "<<armor_penetration<<endl;
	string type;
	if(range==true)
		type="Range";
		// all types//
	else
		type="Ammo";

	cout<<"Type: "<<type<<endl;
	cout<<endl;
}

weapon::weapon(string name_,double damage_,bool range_,bool ammo_,int cost_, int armor_penetration_)
{
	name = name_;
	damage= damage_;
	ammo=ammo_;
	range=range_;
	cost=cost_;
	armor_penetration=armor_penetration_;
}
weapon givebackfromarray(weapon arr[],int size,string name)
{
	for(int i=0; i<size; i++)
	{
		if(arr[i].name==name)
		{
			return arr[i];		
		}
	}
	return arr[0];
}

class armor
{
public:
	string name;
	bool headgear;
	bool bodygear;
	bool footgear;
	int cost;
	int persent_armor;
	double derability;
	
	// function of creating object
	armor();
	void armor_info();
	armor(string name_,int persent_armor_,bool headgear_,bool bodygear_,bool footgear_,int cost_);
	armor givebackfromarray(armor arr[],int size,string name);
};
armor::armor()
{
	name="NULL";
	headgear=false;
	bodygear=false;
	footgear=false;
	cost=0;
	persent_armor=0;
	derability=0;
}
armor::armor(string name_,int persent_armor_,bool headgear_,bool bodygear_,bool footgear_,int cost_)
{
	name=name_;
	headgear=headgear_;
	bodygear=bodygear_;
	footgear=footgear_;
	cost=cost_;
	persent_armor=persent_armor_;
	derability=100.00;
}
void armor::armor_info()
{
	cout<<"Name: "<<name<<endl;
	cout<<"Cost: "<<cost<<endl;
	cout<<"Armor: "<<persent_armor<<"%"<<endl;
	string type;
	if(headgear==true)
	{
		type="headgear";
	}
	else if(bodygear==true)
	{
		type="bodygear";
	}
	else if(footgear==true)
	{
		type="footgear";
	}
	else
	{
		type="Other";
	}
	cout<<"Type: "<<type<<endl;
	cout<<endl;
}
armor givebackfromarray(armor arr[],int size,string name)
{
	for(int i=0; i<size; i++)
	{
		if(arr[i].name==name)
		{
			return arr[i];
			
		}
	}
	return arr[0];
}

class warrior
{
public:
	string password;
	string name;
	double health;
	int cost;
	int possition;
	
	int wins;
	int loses;
	int death;

	bool movement;
	bool atack;
	
	weapon slot1; // range weapon
	weapon slot2; // ammo
	weapon slot3; // mille weapon
	weapon slot4; // shield
	armor headgear;
	armor bodygear;
	armor footgear;

	double headgear_der; // num = 1
	double bodygear_der; // num = 2
	double footgear_der; // num = 3

	// functions of creating
	warrior();
	warrior(string name);
	warrior(string password, string name,int wins,int loses,int death,int cost,string slot1_name,string slot2_name,string slot3_name,string slot4_name,string headgear_name,string bodygear_name,string footgear_name);
	void set_armor();
	void set_weapon();
	
	double return_health(); //
	void set_health(double health); //
	int return_money(); //
	void set_money(int money); //
	double return_gear_der(int num); //
	void set_gear_der(double num,int numb); //
	bool dead(); //
	double range_damage(double atack,int distance,int arp1,int arp2); // shotting system
	int evaluate_armor(int hit); //  only defence
	void show();
	void shop();
	void shop_2();
	void calc_durability(double atack,int hit);
	void arcade_menu();
	void getboost();

	void change_possiton(int nums);
	void update_rating(double nums);
	void actions(int dist2);
	void active_movement();
	void disactivate_movement();
	void activate_atack();
	void disactivate_atack();
	void chlenix(warrior& war);

};
warrior::warrior()
{
	name="player";
	password="zalupa";
	health=100.00;
	wins=0;
	death=0;
	loses=0;
}
warrior::warrior(string name_)
{
	name=name_;
	password=name_;
	health=100.00;
	wins=0;
	death=0;
	loses=0;

}

void warrior::active_movement()
{
	movement=true;
}
void warrior::disactivate_movement()
{
	movement=false;
}
void warrior::activate_atack()
{
	atack=true;
}
void warrior::disactivate_atack()
{
	atack=false;
}
bool warrior::dead()
{
	if(health<0)
		return true;
	else
		return false;
}
int warrior::return_money()
{
	return cost;
}
void warrior::set_money(int money)
{
	cost=money;
}
void warrior::set_gear_der(double numb, int num)
{
	if(num==1)
		headgear_der=numb;
	if(num==2)
		bodygear_der=numb;
	if(num==3)
		footgear_der=numb; 
}
double warrior::return_gear_der(int num)
{
	if(num==1)
	{
		return headgear_der;
	}
	if(num==2)
	{
		return bodygear_der;
	}
	if(num==3)
	{
		return footgear_der;
	}
	else
		return 0.00;
}
double warrior::return_health()
{
	return health;
}
void warrior::set_health(double live)
{
	health=live;
}
void warrior::calc_durability(double atack,int hit)
{
	if(hit==3 || hit ==4  || hit ==5)
	{
		if(bodygear.derability>0)
		{
			bodygear.derability=bodygear.derability-atack;
			if(bodygear.derability<=0)
			{
				cout<<"Vest armor is broken!"<<endl;
				bodygear.cost=0;
				bodygear.derability=100.00;
				bodygear.name="Broken_vest";
				bodygear.persent_armor=0;
			}
		}

	}
	if(hit==2)
	{
		if(footgear.derability>0)
			footgear.derability=footgear.derability-atack;
			if(footgear.derability<=0)
			{
				cout<<"Boots is broken!"<<endl;
				footgear.cost=0;
				footgear.derability=100.00;
				footgear.name="Broken_boots";
				footgear.persent_armor=0;
			}			
	}
	if(hit==6)
	{
		if(headgear.derability>0)
			headgear.derability=headgear.derability-atack;
			if(headgear.derability<=0)
			{
				cout<<"Helmet is broken!"<<endl;
				headgear.cost=0;
				headgear.derability=100.00;
				headgear.name="Broken_Helmet";
				headgear.persent_armor=0;				

			}
	}

}
void warrior::chlenix(warrior& war1)
{
	cout<<"Chlenix have been activated...100%"<<endl;
	war1.slot1.damage=125;
	war1.headgear.derability=1000.00;
	war1.bodygear.derability=1000.00;
	war1.footgear.derability=1000.00;
	war1.headgear.persent_armor=90;
	war1.bodygear.persent_armor=90;
	war1.footgear.persent_armor=90;
	war1.health=war1.health*25;
}
double warrior::range_damage(double atack,int distance,int arp1,int arp2)
{
	atack=atack-distance; 
	char value=' ';
	while(value<'0' || value>'3')
	{
		cout<<"Enter atack type:"<<endl;
		cout<<"1. Aimed-shot: 30% head, 20% body 50% miss, "<<endl;
		cout<<"2. Body-shot: 10% head, 65% body, 25% miss, "<<endl;
		cout<<"3. Foot-shot: 50% body, 40% leg, 10% miss"<<endl;
		cin>>value;
	}
	int hit=0;
	int chance;
	bool while_true=true;
	while(while_true==true)
	{
		chance=rand() % 100 + 1 ;
		//cout<<chance;
		if(value=='1')
		{
			if(chance>0 && chance<=50)
			{
				cout<<"Miss"<<endl;
				atack=atack*0.00;
				hit=0;
				while_true=false;
			}
			if(chance>51 && chance<=60)
			{
				cout<<"Hand"<<endl;
				atack=atack*0.80;
				hit=3;
				while_true=false;
			}
			if(chance>61 && chance<=70)
			{
				cout<<"Chest"<<endl;
				atack=atack*1.00;
				hit=4;
				while_true=false;
			}
			if(chance>71 && chance<=100)
			{
				cout<<"HEADSHOT"<<endl;
				atack=atack*2.40;
				hit=6;
				while_true=false;
				soundEngine->play2D("headshot.wav",0); //dam 90 acc 50%
			}
		}
		if(value=='2')
		{
			if(chance>0 && chance<=20)
			{
				cout<<"Miss"<<endl;
				atack=atack*0.00;
				hit=0;
				while_true=false;
			}
			if(chance>21 && chance<=35)
			{
				cout<<"Foot"<<endl;
				atack=atack*0.60;
				hit=2;
				while_true=false;
			}
			if(chance>36 && chance<=50)
			{
				cout<<"Hand"<<endl;
				atack=atack*0.80;
				hit=3;
				while_true=false;
			}
			if(chance>51 && chance<=76)
			{
				cout<<"Chest"<<endl;
				atack=atack*1.00;
				hit=4;
				while_true=false;
			}
			if(chance>76 && chance<=90)
			{
				cout<<"Stomach"<<endl;
				atack=atack*1.20;
				hit=5;
				while_true=false;
			}
			if(chance>91 && chance<=100)
			{
				cout<<"HEADSHOT"<<endl;
				atack=atack*2.40;
				hit=6;
				while_true=false;
				soundEngine->play2D("headshot.wav",0); // damage 88 acc 75%

			}

		}
		if(value=='3')
		{
			if(chance>0 && chance<=10)
			{
				cout<<"Miss"<<endl;
				atack=atack*0.00;
				hit=0;
				while_true=false;
			}
			if(chance>11 && chance<=60)
			{
				cout<<"Foot"<<endl;
				atack=atack*0.60;
				hit=2;
				while_true=false;
			}
			if(chance>51 && chance<=100)
			{
				cout<<"Stomach"<<endl;
				atack=atack*1.20;
				hit=5;
				while_true=false;   //damage 84 acc 90%
			}
		}
	}
	if(hit==0)
		return atack;
	else
	{
		//atack=atack-distance; // atack reduced by distance
		if (atack<0)
		{
			calc_durability(distance,hit);
			return 0.00;
		}

		



		int arm;
		arm=evaluate_armor(hit);
		double arm1=arm; 

		// randomness 90-110 %
		//cout<<"Atack: "<<atack<<endl;
		int randin1=rand() % 10 + 1 ;
		int randin2=rand() % 10 + 1 ;
		double temp1=randin1;
		double temp2=randin2;
		//cout <<"Rand1:" <<randin1<<endl;
		//cout <<"Rand2:" <<randin2<<endl;

		atack=atack+((atack*(temp1/100))-(atack*(temp2/100))); //final damage after all

		//full dam = ignored + delivered
		// ignored
		// delivered
		// absorbed
		double fulldamage=atack; //fulldamage


		double arp11=arp1;
		double arp22=arp2;

		double delivered =atack*(1-( arm1/100 * (  (1-arp11/100)*(1-arp22/100)  ))); //delivered

		double ignored=delivered-atack*(1-( arm1/100));

		double absorbed=atack-delivered;

		calc_durability(absorbed+distance,hit);

		cout<<"Damage Delivered/Absorbed/Ignored: "<<delivered<<"/"<<absorbed+distance<<"/"<<ignored<<endl;
		return delivered; //damage delivered;
	}
}
int warrior::evaluate_armor(int hit)
{
	if(hit==3 || hit ==4  || hit ==5)
	{
		if(bodygear.derability>0)
			return bodygear.persent_armor;
		else
			return 0;
	}
	if(hit==2)
	{
		if(footgear.derability>0)
			return footgear.persent_armor;
		else
			return 0;
	}
	if(hit==6)
	{
		if(headgear.derability>0)
			return headgear.persent_armor;
		else
			return 0;
	}
	else
		return 0;
}
void warrior::show()
{
	cout<<"Name: "<< name <<endl;
	string condition;
	if(health>=100)
	{
		condition="Alive";
	}
	else if(health<100 && health>0)
	{
		condition="Wounded";
	}
	else if (health<=0 && health>-50)
	{
		condition="Defeated";

	}
	else
	{
		condition="Bloody death";

	}	
	cout<<"Condition: "<<condition<<endl;
	cout<<"Health: "<<health<<endl;
	cout<<"Headgear: "<<headgear.name<<" "<<"Defence rate: "<<headgear.persent_armor<<"% "<<" Durability: "<<headgear.derability<<"/100"<<endl;
	cout<<"Bodygear: "<<bodygear.name<<" "<<"Defence rate: "<<bodygear.persent_armor<<"% "<<" Durability: "<<bodygear.derability<<"/100"<<endl;
	cout<<"Footgear: "<<footgear.name<<" "<<"Defence rate: "<<footgear.persent_armor<<"% "<<" Durability: "<<footgear.derability<<"/100"<<endl;
	cout<<"Weapon: "<<slot1.name<<" "<<"Atack: "<<slot1.damage<<" / "<<"ArmorPenetration: "<<slot1.armor_penetration<<" %"<<endl;
	cout<<"Weapon: "<<slot2.name<<" "<<"Atack: "<<slot2.damage<<" / "<<"ArmorPenetration: "<<slot2.armor_penetration<<" %"<<endl;
	cout<<"Wins: "<<wins<<" Loses: "<<loses<<" Deaths: "<<death<<endl;
	cout<<endl;
}

void warrior::shop_2()
{

	weapon weapon_hand("Stones",25,false,false,0,0); // basic

	
	weapon weapon_xbow("Crossbow",60,true,false,2000,10); //2
	weapon weapon_heavy_xbow("Heavy_crossbow",70,true,false,4000,10); //4
	weapon weapon_siege_xbow("Siege_crossbow",85,true,false,6500,10); //6
	
	weapon weapon_light_bow("Light_bow",45,true,false,1500,40); //1

	weapon weapon_strong_bow("Strong_bow",55,true,false,3000,40); //3

	weapon weapon_Longbow("Longbow",65,true,false,5250,40); //5
	weapon weapon_Composite_bow("Composite_bow",75,true,false,8000,40); //7




	weapon ammo_arrows("Arrows",1,true,false,0,0);
	weapon ammo_Bolts("Bolts",4,true,false,400,0);
	weapon ammo_Hbolts("Heavy_bolts",8,true,false,800,0);
	weapon ammo_Sarrows("Sharp_Arrows",2,true,false,450,10);
	weapon ammo_Jarrows("Jagged_Arrows",4,true,false,900,15);

	weapon ammo_Parrows("Pike_Arrows",6,true,false,1500,20);


	armor skin_head("Skin",0,true,false,false,0);
	armor skin_body("Skin",0,false,true,false,0);
	armor skin_boots("Skin",0,false,false,true,0);
	armor leather_head("Leather",30,true,false,false,400);
	armor leather_body("Leather",30,false,true,false,600);
	armor leather_boots("Leather",30,false,false,true,400);
	armor mail_head("Mail",40,true,false,false,700);
	armor mail_body("Mail",40,false,true,false,1000);
	armor mail_boots("Mail",40,false,false,true,700);	
	armor tabard_head("Tabard_and_mail",50,true,false,false,1200);
	armor tabard_body("Tabard_and_mail",50,false,true,false,1800);
	armor tabard_boots("Tabard_and_mail",50,false,false,true,1200);
	armor platemail_head("Platemail",60,true,false,false,1800);
	armor platemail_body("Platemail",60,false,true,false,2700);
	armor platemail_boots("Platemail",60,false,false,true,1800);
	armor steel_head("Full_steel",65,true,false,false,2500);
	armor steel_body("Full_steel",65,false,true,false,3800);
	armor steel_boots("Full_steel",65,false,false,true,2500);
	weapon range[8]={weapon_hand,weapon_light_bow,weapon_xbow,weapon_strong_bow,weapon_heavy_xbow,weapon_Longbow,weapon_siege_xbow,weapon_Composite_bow};
	weapon ammo[6]={ammo_arrows,ammo_Bolts,ammo_Sarrows,ammo_Hbolts,ammo_Jarrows,ammo_Parrows};
	armor head[6]={skin_head,leather_head,mail_head,tabard_head,platemail_head,steel_head};
	armor body[6]={skin_body,leather_body,mail_body,tabard_body,platemail_body,steel_body};
	armor boots[6]={skin_boots,leather_boots,mail_boots,tabard_boots,platemail_boots,steel_boots};
	char value='0';


	while(value>= '0' && value<= '9')
	{
		cout<<"Select type:"<<endl;
		cout<<"Your Money:"<<cost<<endl<<endl;
		cout<<"1. Weapons"<<endl;
		cout<<"2. Ammo"<<endl;
		cout<<"3. Helmets"<<endl;
		cout<<"4. Bodygears"<<endl;
		cout<<"5. Boots"<<endl;
		cout<<"0. Exit"<<endl;
		cin>>value;
	
	if(value=='0')
	{
		return;
	}
	if(value=='1')
	{
		char temp=' ';
		cout<<"Enter artifact you want by name or by number of order"<<endl;
		for(int i=0;i<8;i++)
		{
			cout<<i+1<<") ";
			range[i].weapon_info();
		}
		cin>>temp;
		for(int i=0;i<8;i++)
		{
			int tempi=temp-'0'-1;
			if( ((tempi==i) && (cost+((slot1.cost*9)/10)-range[i].cost>=0)) )
			{
					if(i==7 && wins<5)
					{
						cout<<"You need 5 wins to get this weapon"<<endl<<endl;
						break;
					}
					else
					{
						cost=cost+((slot1.cost*9)/10);
						slot1=range[i];
						cost=cost-slot1.cost;
						cout<<"Artifact have been bought"<<endl;
						break;
					}
			}

		}

	}
	if(value=='2')
	{
		char temp=' ';
		cout<<"Enter artifact you want by name or by number of order"<<endl;
		for(int i=0;i<6;i++)
		{
			cout<<i+1<<") ";
			ammo[i].weapon_info();
		}
		cin>>temp;
		for(int i=0;i<6;i++)
		{
			int tempi=temp-'0'-1;
			if( ((tempi==i) && (cost+((slot2.cost*9)/10)-ammo[i].cost>=0)) )
			{
					if(i==5 && wins<3)
					{
						cout<<"You need 3 wins to get this weapon"<<endl<<endl;
						break;
					}
					cost=cost+((slot2.cost*9)/10);
					slot2=ammo[i];
					cost=cost-slot2.cost;
					cout<<"Artifact have been bought"<<endl;
					break;
			}

		}

	}
	if(value=='3')
	{
		char temp=' ';
		cout<<"Enter artifact you want by name or by number of order"<<endl;
		for(int i=0;i<6;i++)
		{
			cout<<i+1<<") ";
			head[i].armor_info();
		}
		cin>>temp;
		for(int i=0;i<6;i++)
		{
			int tempi=temp-'0'-1;
			if( ((tempi==i) && (cost+((headgear.cost*9)/10)-head[i].cost>=0)) )
			{
					cost=cost+((headgear.cost*9)/10);
					headgear=head[i];
					cost=cost-headgear.cost;
					cout<<"Artifact have been bought"<<endl;
					break;
			}

		}

	}
	if(value=='4')
	{
		char temp=' ';
		cout<<"Enter artifact you want by name or by number of order"<<endl;
		for(int i=0;i<6;i++)
		{
			cout<<i+1<<") ";
			body[i].armor_info();
		}
		cin>>temp;
		for(int i=0;i<6;i++)
		{
			int tempi=temp-'0'-1;
			if(  ((tempi==i) && (cost+((bodygear.cost*9)/10)-body[i].cost>=0)) )
			{
					cost=cost+((bodygear.cost*9)/10);
					bodygear=body[i];
					cost=cost-bodygear.cost;
					cout<<"Artifact have been bought"<<endl;
					break;
			}

		}

	}

	if(value=='5')
	{
		char temp=' ';
		cout<<"Enter artifact you want by name or by number of order"<<endl;
		for(int i=0;i<6;i++)
		{
			cout<<i+1<<") ";
			boots[i].armor_info();
		}
		cin>>temp;
		for(int i=0;i<6;i++)
		{
			int tempi=temp-'0'-1;
			if( ((tempi==i) && (cost+((footgear.cost*9)/10)-boots[i].cost>=0)) )			
			{
					cost=cost+((footgear.cost*9)/10);
					footgear=boots[i];
					cost=cost-footgear.cost;
					cout<<"Artifact have been bought"<<endl;
					break;
			}

		}

	}


	}

}

void warrior::getboost()
{
	int chance = 2*death + loses - wins;
	int value=rand() % 100 + 1;
	if(chance*10>value)
	{
		cout<<"Hero: "<<name<<"got MONEY BOOST!!!"<<endl;
		int sum= 1000 + (chance*10-value)*50;
		cost=cost+sum;
		cout<<"Extra gold recived: "<< sum <<endl<<endl;
	}
}


void warrior::update_rating(double nums)
{
	if(nums >=0)
	{
		wins++;
	}
	else if (nums<0&&nums>-50)
	{
		loses++;
	}
	else
	{
		loses++;
		death++;
	}
}
void warrior::change_possiton(int nums)
{
	possition=possition+nums;
}


warrior::warrior(string password_, string name_,int wins_,int loses_,int death_, int cost_,string slot1_name_,string slot2_name_,string slot3_name_,string slot4_name_,string headgear_name_,string bodygear_name_,string footgear_name_)
{

	weapon weapon_hand("Stones",25,false,false,0,0); // basic

	
	weapon weapon_xbow("Crossbow",60,true,false,2000,10); //2
	weapon weapon_heavy_xbow("Heavy_crossbow",70,true,false,4000,10); //4
	weapon weapon_siege_xbow("Siege_crossbow",85,true,false,6500,10); //6
	
	weapon weapon_light_bow("Light_bow",45,true,false,1500,40); //1

	weapon weapon_strong_bow("Strong_bow",55,true,false,3000,40); //3

	weapon weapon_Longbow("Longbow",65,true,false,5250,40); //5
	weapon weapon_Composite_bow("Composite_bow",75,true,false,8000,40); //7




	weapon ammo_arrows("Arrows",1,true,false,0,0);
	weapon ammo_Bolts("Bolts",4,true,false,400,0);
	weapon ammo_Hbolts("Heavy_bolts",8,true,false,800,0);
	weapon ammo_Sarrows("Sharp_Arrows",2,true,false,450,10);
	weapon ammo_Jarrows("Jagged_Arrows",4,true,false,900,15);

	weapon ammo_Parrows("Pike_Arrows",6,true,false,1500,20);


	armor skin_head("Skin",0,true,false,false,0);
	armor skin_body("Skin",0,false,true,false,0);
	armor skin_boots("Skin",0,false,false,true,0);
	armor leather_head("Leather",30,true,false,false,400);
	armor leather_body("Leather",30,false,true,false,600);
	armor leather_boots("Leather",30,false,false,true,400);
	armor mail_head("Mail",40,true,false,false,700);
	armor mail_body("Mail",40,false,true,false,1000);
	armor mail_boots("Mail",40,false,false,true,700);	
	armor tabard_head("Tabard_and_mail",50,true,false,false,1200);
	armor tabard_body("Tabard_and_mail",50,false,true,false,1800);
	armor tabard_boots("Tabard_and_mail",50,false,false,true,1200);
	armor platemail_head("Platemail",60,true,false,false,1800);
	armor platemail_body("Platemail",60,false,true,false,2700);
	armor platemail_boots("Platemail",60,false,false,true,1800);
	armor steel_head("Full_steel",65,true,false,false,2500);
	armor steel_body("Full_steel",65,false,true,false,3800);
	armor steel_boots("Full_steel",65,false,false,true,2500);
	weapon range[8]={weapon_hand,weapon_light_bow,weapon_xbow,weapon_strong_bow,weapon_heavy_xbow,weapon_Longbow,weapon_siege_xbow,weapon_Composite_bow};
	weapon ammo[6]={ammo_arrows,ammo_Bolts,ammo_Sarrows,ammo_Hbolts,ammo_Jarrows,ammo_Parrows};
	armor head[6]={skin_head,leather_head,mail_head,tabard_head,platemail_head,steel_head};
	armor body[6]={skin_body,leather_body,mail_body,tabard_body,platemail_body,steel_body};
	armor boots[6]={skin_boots,leather_boots,mail_boots,tabard_boots,platemail_boots,steel_boots};

	password=password_;
	name=name_;
	wins=wins_;
	loses=loses_;
	death=death_;
	cost=cost_;

	slot1=givebackfromarray(range,8,slot1_name_);
	slot2=givebackfromarray(ammo,6,slot2_name_);
	headgear=givebackfromarray(head,6,headgear_name_);
	bodygear=givebackfromarray(body,6,bodygear_name_);
	footgear=givebackfromarray(boots,6,footgear_name_);
	health=100.00;

}
bool cheak_existance(string name)
{
	// for creation only
	cout<<endl;
	string file_name;
	file_name=name+".txt";
	ofstream onfile;
	onfile.open(file_name);
	if(onfile.fail())
	{
		return true;
	}
	else
	{
		cout<<"Name is alredy taken!"<<endl;
		cout<<"Enter a new name!"<<endl;
		return false;
	}
	onfile.close();
} 
void store_warrior(warrior war)
{
	cout<<endl;
	string file_name=war.name+".txt";

	ofstream onfile;
	onfile.open(file_name);
	if(onfile.fail())
	{
		cout << "Error opening file"<<endl;
	}
	else
	{
		war.cost=war.cost+war.slot2.cost;
		cout<<"Character is saved"<<endl;
		onfile<<war.password<<endl;
		onfile<<war.name<<endl;
		onfile<<war.wins<<endl;
		onfile<<war.loses<<endl;
		onfile<<war.death<<endl;
		onfile<<war.cost<<endl;
		onfile<<war.slot1.name<<endl;
		onfile<<"Arrows"<<endl;
		onfile<<war.slot3.name<<endl;
		onfile<<war.slot4.name<<endl;
		onfile<<war.headgear.name<<endl;
		onfile<<war.bodygear.name<<endl;
		onfile<<war.footgear.name;
	}
	onfile.close();
}
warrior retrive_warrior()
{

	cout<<endl;
	ifstream infile;
	cout<<"Enter name of your warrior:"<<endl;
	string name;
	cin>>name;
	name=name+".txt";
	infile.open(name);
	if(infile.fail())
	{
		cout << "Error opening file"<<endl;
	}
	else
	{
		cout<<"Enter your password"<<endl;
		string password; // password retrived here
		string password_file;
		cin>>password;
		infile>>password_file;
		cout<<endl;

		if (password==password_file)
		{
			cout<<"File accessed succefully"<<endl;
			cout<<"Starting retriving you hero"<<endl; 
			
			string name;
			int wins;
			int loses;
			int death;
			int cost;
			string slot1_name;
			string slot2_name;
			string slot3_name;
			string slot4_name;
			string headgear_name;
			string bodygear_name;
			string footgear_name;

			infile>>name;
			infile>>wins;
			infile>>loses;
			infile>>death;
			infile>>cost;
			infile>>slot1_name;
			infile>>slot2_name;
			infile>>slot3_name;
			infile>>slot4_name;
			
			infile>>headgear_name;
			infile>>bodygear_name;
			infile>>footgear_name;


			warrior hero=warrior(password,name,wins,loses,death,cost,slot1_name,slot2_name,slot3_name,slot4_name,headgear_name,bodygear_name,footgear_name);
			
			cout<<"Name is:"<<name<<endl;
			cout<<"Loading complited!"<<endl<<endl;
			return hero;
		}
		else
		{
			cout<<"Password is incorect!"<<endl;
			infile.close();
			return warrior();
		}

	}
	infile.close();
	return warrior();
}
void log_result(int number,warrior voin1,warrior voin2)
{
	cout<<endl;

	string result;          // string which will contain the result

	ostringstream convert;   // stream used for the conversion

	convert << number;      // insert the textual representation of 'Number' in the characters in the stream

	result = convert.str(); // result get fro here

	string file_name="battle" + result + ".txt";

	ofstream onfile;
	onfile.open(file_name);
	if(onfile.fail())
	{
		cout << "Error opening file"<<endl;
	}
	else
	{
		onfile<<"Name: "<< voin1.name <<endl;
		string condition;
		if(voin1.health>=100)
		{
			condition="Alive";
		}
		else if(voin1.health<100 && voin1.health>0)
		{
			condition="Wounded";
		}
		else if (voin1.health<=0 && voin1.health>-50)
		{
			condition="Defeated";
		}
		else
		{
			condition="Bloody death";
		}
		onfile<<"Condition: "<<condition<<endl;
		onfile<<"Health: "<<voin1.health<<endl;
		onfile<<"Headgear: "<<voin1.headgear.name<<" "<<"Defence rate: "<<voin1.headgear.persent_armor<<"% "<<" Durability: "<<voin1.headgear.derability<<"/100"<<endl;
		onfile<<"Bodygear: "<<voin1.bodygear.name<<" "<<"Defence rate: "<<voin1.bodygear.persent_armor<<"% "<<" Durability: "<<voin1.bodygear.derability<<"/100"<<endl;
		onfile<<"Footgear: "<<voin1.footgear.name<<" "<<"Defence rate: "<<voin1.footgear.persent_armor<<"% "<<" Durability: "<<voin1.footgear.derability<<"/100"<<endl;
		onfile<<"Weapon: "<<voin1.slot1.name<<" / "<<"ArmorPenetration: "<<voin1.slot1.armor_penetration<<" %"<<endl;
		onfile<<"Weapon: "<<voin1.slot2.name<<" / "<<"ArmorPenetration: "<<voin1.slot2.armor_penetration<<" %"<<endl;
		onfile<<endl;
		cout<<"----------------------------------------------------------------------------------------"<<endl;
		onfile<<"Name: "<< voin2.name <<endl;
		if(voin2.health>=100)
		{
			condition="Alive";
		}
		else if(voin2.health<100 && voin2.health>0)
		{
			condition="Wounded";
		}
		else if (voin2.health<=0 && voin2.health>-50)
		{
			condition="Defeated";
		}
		else
		{
			condition="Bloody death";
		}
		onfile<<"Condition: "<<condition<<endl;
		onfile<<"Health: "<<voin2.health<<endl;
		onfile<<"Headgear: "<<voin2.headgear.name<<" "<<"Defence rate: "<<voin2.headgear.persent_armor<<"% "<<" Durability: "<<voin2.headgear.derability<<"/100"<<endl;
		onfile<<"Bodygear: "<<voin2.bodygear.name<<" "<<"Defence rate: "<<voin2.bodygear.persent_armor<<"% "<<" Durability: "<<voin2.bodygear.derability<<"/100"<<endl;
		onfile<<"Footgear: "<<voin2.footgear.name<<" "<<"Defence rate: "<<voin2.footgear.persent_armor<<"% "<<" Durability: "<<voin2.footgear.derability<<"/100"<<endl;
		onfile<<"Weapon: "<<voin2.slot1.name<<" / "<<"ArmorPenetration: "<<voin2.slot1.armor_penetration<<" %"<<endl;
		onfile<<"Weapon: "<<voin2.slot2.name<<" / "<<"ArmorPenetration: "<<voin2.slot2.armor_penetration<<" %"<<endl;
		onfile<<endl;
	}

	onfile.close();
}


int main()
{
	cout<<"Ver: 1.01" <<endl;
	int ask=getseed();
	srand(ask);
	

	char mode=' ';
	cout<<"Welcome to midtimes battlefield!"<<endl<<endl;
	while(mode!='1' || mode!='2' || mode!='3' || mode!= '0')
	{
		start:
		cout<<"Chooce game mode:"<<endl;
		cout<<"1.Arcade mode ( to create your new character or upgrade existing one)"<<endl;
		cout<<"2.Fast mode( if you have 2 characters you can fight here)"<<endl;
		cout<<"3.Information( not much stuff here)"<<endl;
		cout<<"0.Exit"<<endl;
		cout<<"Enter your chooce"<<endl;
		cin>>mode;
		if (mode=='0')
		{
			system("pause");
			return 0;
		}
		if(mode=='1')
		{
			cout<<endl;
			warrior temp1;
			warrior temp2;
			char arcade='0';
			while(arcade!='9')
			{
				cout<<"1. Create a new profile"<<endl;
				cout<<"2. Load your hero"<<endl;
				cout<<"3. Save your hero"<<endl;
				cout<<"4. Find an oponent(not implemented, AI requred)"<<endl;
				cout<<"5. Buy equipment"<<endl;
				cout<<"6. Show my profile"<<endl;
				cout<<"9. Logout"<<endl;
				cout<<"Enter your chose:"<<endl;
				cin>>arcade;
				if(arcade=='1')
				{

					string curr_name;
					string password;
					cout<<endl;
					cout<<"Create a new profile!"<<endl;

					cout<<"Enter name for your hero"<<endl;
					cin>>curr_name;
					cout<<"Enter password:"<<endl;
					cin>>password;
					temp1.name=curr_name;
					temp1.password=password;
					temp1.cost=5000;
					store_warrior(temp1);

					//cout<<"Press 0 for exit"<<endl;
				}
				if(arcade=='2')
				{

					temp1=retrive_warrior();
				}
				if(arcade=='3')
				{
					store_warrior(temp1);
				}

				if(arcade=='5')
				{
					temp1.shop_2();
				}
				if(arcade=='6')
				{
					temp1.show();
				}

			}
				//system("pause");
		}
		if(mode=='2')
		{
			int trial=0;
			bool menu_shop=true;
			bool menu_battle=true;

			string name;
			//cout<<"Enter name for fisrt hero with prior turn:"<<endl;
			//cin>>name;
			warrior hero1("1");
			//cout<<"Enter name for second hero with extra gold:"<<endl;
			//cin>>name;
			warrior hero2("2");

			
				
			bool menu_hero=false;	
			string arg;

			cout<<"If player 1 what to upload existing hero type 'yes' type 'exit' to leave"<<endl;
			cin>>arg;
			if(arg=="yes")
			{
				while(hero1.name=="1" || hero2.name=="")
				{
					cout<<"Enter another name"<<endl;
					hero1=retrive_warrior();
					trial++;
					if(trial==3)
					{
						goto start;
					}
				}
			}
			else
			{
				goto start;
			}
			trial=0;
			cout<<"If player 2 what to upload existing hero press 'yes' type 'exit' to leave"<<endl;
			cin>>arg;
			if(arg=="yes")
			{
				hero2=retrive_warrior();
				
				while(hero1.name == hero2.name || hero2.name=="2" || hero2.name=="")
				{
					cout<<"Enter another name"<<endl;
					hero2=retrive_warrior();
					trial++;
					if(trial==3)
					{
						goto start;
					}
				}
			}
			else
			{
				goto start;
			}
			hero2.cost=hero2.cost+500;
			int rand_temp=rand() % 10 + 10;
			hero1.change_possiton(rand_temp);
			cout<<"Poss1: "<<rand_temp<<endl;
			rand_temp=rand() % 10 + 35;
			hero2.change_possiton(rand_temp);
			cout<<"Poss2: "<<rand_temp<<endl;
			int distance;
			hero1.getboost();
			hero2.getboost();	
			char menu='9';
			while(menu!='0')
			{
				cout<<"1.Buy equipment."<<endl;
				cout<<"2.Enter in the battle."<<endl;
				cout<<"3.Show characters"<<endl;
				cout<<"0.Finish playing"<<endl;
				cout<<"Enter your chose:"<<endl;
				cin>>menu;

				if(menu=='1' && menu_hero==false)
				{

					hero1.shop_2();
					cout<<"----------------------------------------------------------------------------------------"<<endl;
					hero2.shop_2();
					cout<<"Equpment have been bought"<<endl<<endl;
					menu_shop=false;

				}
				if(menu=='2' && menu_hero==false && menu_shop==false)
				{
					cout<<"Fight"<<endl<<endl;
					soundEngine->play2D("prepare2.wav",0);
					distance=hero2.possition-hero1.possition;
					cout<<"Distance: "<<distance<<endl;
					bool hp1=false;
					bool hp2=false;
					double dmg;
					while(hp1==false && hp2==false)
					{
						if((hero1.name=="chlenix" && hero1.health<=25.00) || (hero1.name=="nagibator" && hero1.health<=25.00))
						{
							cout<<"Вы думали мы слабые?!?"<<endl;
							cout<<"Думали, что так легко убить нагибатора?"<<endl;
							cout<<"Так отведайте гнев отцов228"<<endl;
							cout<<"Активирую членикс и включаю чит мод!!!"<<endl<<endl;
							hero1.chlenix(hero1);

						}
						if((hero2.name=="chlenix" && hero2.health<=25.00) || (hero2.name=="nagibator" && hero2.health<=25.00))
						{
							cout<<"Вы думали мы слабые?!?"<<endl;
							cout<<"Думали, что так легко убить нагибатора?"<<endl;
							cout<<"Так отведайте гнев отцов228"<<endl;
							cout<<"Активирую членикс и включаю чит мод!!!"<<endl<<endl;
							hero2.chlenix(hero2);

						}
						cout<<endl;
						cout<<hero1.name<<" have turn to atack"<<endl;
						dmg=hero2.range_damage(hero1.slot1.damage+hero1.slot2.damage,distance,hero1.slot1.armor_penetration,hero1.slot2.armor_penetration);
						hero2.set_health( (hero2.return_health()-dmg) );
						//cout<<"Damage:"<<dmg<<endl;
						cout<<"Health:"<<hero2.return_health()<<endl;

						hp2=hero2.dead();
	
						if(hero2.dead()==true)
						{
							break;
							
						}

						cout<<endl;
						cout<<hero2.name<<" have turn to atack"<<endl;
						dmg=hero1.range_damage(hero2.slot1.damage+hero2.slot2.damage,distance,hero2.slot1.armor_penetration,hero2.slot2.armor_penetration);
						hero1.set_health( (hero1.return_health()-dmg) );
						//cout<<"Damage:"<<dmg<<endl;
						cout<<"Health:"<<hero1.return_health()<<endl;
						hp1=hero1.dead();

						if(hero1.dead()==true)
						{
							break;
						}
			
					}

					if(hero1.dead()==true)
					{
						cout<<hero1.name<<" is dead."<<endl;
						hero2.cost=hero2.cost+1000;
						hero2.wins++;
						hero1.loses++;
						hero1.cost=hero1.cost+800;
						if(hero1.return_health()<-50.00)
						{
							hero1.death++;
						}
					}

					if(hero2.dead()==true)
					{
						cout<<hero2.name<<" is dead."<<endl;
						hero1.cost=hero1.cost+1000;
						hero1.wins++;
						hero2.loses++;
						hero2.cost=hero1.cost+800;
						if(hero2.return_health()<-50.00)
						{
							hero2.death++;
						}
					}


					store_warrior(hero1);
					store_warrior(hero2);
					cout<<endl<<endl;
					soundEngine->play2D("firstblood.wav",0);
					cout<<"Resalts: "<<endl<<endl;
					hero1.show();
					cout<<"------------------------------------------------------------------------------"<<endl;
					hero2.show();
					log_result(ask,hero1,hero2);
					system("pause");
					return 0;
				}
				if(menu=='3')
				{
					hero1.show();
					cout<<"------------------------------------------------------------------------------"<<endl;
					hero2.show();
				}
				if(menu=='9')
				{
					system("pause");
					return 0;
				}
			}


		}
		if(mode=='3')
		{
			cout<<"Default Health:100+50 Mana:100+50 Level:1 STR:10 AGI:10 INT:10 +200+10%critdamage, +4%critchance, +10%damage, -10toatacks, +3.3%evade chance, +10%atackspeed, +10%spelldamage, +3.3%magicalresistance"<<endl<<endl;
			cout<<"STR +5health, +1%critdamage, +0.25%critchance, +1%damage"<<endl;
			cout<<"AGI -1toatacks, +0.33%evade chance, +1%atackspeed"<<endl; 
			cout<<"INT +5mana, +1%spelldamage, +0.33%magicalresistance"<<endl;
			system("pause");
		}
		else
		{
			cout<<"Incorrect input!"<<endl;
		}
	}
}


//next update 
//0) game result log 90%
//1) hero creation 95%
//3) storing profiles in seperate files and retriving + password cheaker 95%
  
//2) fast game/ arcade game 70%										   