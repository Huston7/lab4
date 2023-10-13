#include <iostream>
#include <vector>
#include <string>
#include <ctime>

// Базовий клас "Живий організм"
using System.Drawing;

class LivingOrganism
{
    public:
    int energy;
    int age;
    int size;

    LivingOrganism(int energy, int age, int size) : energy(energy), age(age), size(size) { }

    virtual void consumeEnergy(int amount)
    {
        energy -= amount;
    }

    virtual void grow()
    {
        age++;
        size++;
    }
};

// Інтерфейс "Можливість розмноження"
class IReproducible
{
    public:
    virtual void reproduce() = 0;
};

// Інтерфейс "Можливість полювання"
class IPredator
{
    public:
    virtual void hunt(LivingOrganism* prey) = 0;
};

// Клас "Тварина"
class Animal : public LivingOrganism, public IReproducible, public IPredator
{
public:
    std::string species;
Animal(int energy, int age, int size, const std::string& species) : LivingOrganism(energy, age, size), species(species) { }

void reproduce() override
{
    std::cout << "Animal of species " << species << " is reproducing." << std::endl;
}

void hunt(LivingOrganism* prey) override
{
    std::cout << "Animal of species " << species << " is hunting." << std::endl;
    consumeEnergy(10);
}
};

// Клас "Рослина"
class Plant : public LivingOrganism, public IReproducible
{
public:
    std::string type;
Plant(int energy, int age, int size, const std::string& type) : LivingOrganism(energy, age, size), type(type) { }

void reproduce() override
{
    std::cout << "Plant of type " << type << " is reproducing." << std::endl;
}
};

// Клас "Мікроорганізм"
class Microorganism : public LivingOrganism, public IReproducible, public IPredator
{
public:
    std::string strain;
Microorganism(int energy, int age, int size, const std::string& strain) : LivingOrganism(energy, age, size), strain(strain) { }

void reproduce() override
{
    std::cout << "Microorganism of strain " << strain << " is reproducing." << std::endl;
}

void hunt(LivingOrganism* prey) override
{
    std::cout << "Microorganism of strain " << strain << " is hunting." << std::endl;
    consumeEnergy(5);
}
};

// Клас "Екосистема"
class Ecosystem
{
    public:
    std::vector<LivingOrganism*> organisms;

    void addOrganism(LivingOrganism* organism)
    {
        organisms.push_back(organism);
    }

    void simulateEcosystem(int numIterations)
    {
        for (int i = 0; i < numIterations; i++)
        {
            std::cout << "Ecosystem Simulation - Iteration " << i + 1 << std::endl;
            for (LivingOrganism* organism : organisms)
            {
                organism->grow();
                if (dynamic_cast<IReproducible*>(organism))
                {
                    dynamic_cast<IReproducible*>(organism)->reproduce();
                }
                if (dynamic_cast<IPredator*>(organism))
                {
                    if (organisms.size() > 1)
                    {
                        int preyIndex = rand() % organisms.size();
                        while (organisms[preyIndex] == organism)
                        {
                            preyIndex = rand() % organisms.size();
                        }
                        dynamic_cast<IPredator*>(organism)->hunt(organisms[preyIndex]);
                        if (organisms[preyIndex]->energy <= 0)
                        {
                            delete organisms[preyIndex];
                            organisms.erase(organisms.begin() + preyIndex);
                        }
                    }
                }
            }
            std::cout << "Ecosystem State after Iteration " << i + 1 << ":" << std::endl;
            for (LivingOrganism* organism : organisms)
            {
                std::cout << "Organism: Age=" << organism->age << ", Energy=" << organism->energy << ", Size=" << organism->size << std::endl;
            }
        }
    }
};

int main()
{
    srand(static_cast<unsigned>(time(0));

    Animal lion(100, 5, 4, "Lion");
Animal zebra(80, 4, 3, "Zebra");
Plant oak(50, 10, 15, "Oak");
Microorganism bacteria(30, 1, 1, "Bacteria");

Ecosystem ecosystem;
ecosystem.addOrganism(&lion);
ecosystem.addOrganism(&zebra);
ecosystem.addOrganism(&oak);
ecosystem.addOrganism(&bacteria);

ecosystem.simulateEcosystem(10);

return 0;
}