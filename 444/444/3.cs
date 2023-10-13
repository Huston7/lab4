#include <iostream>
#include <vector>
#include <chrono>
#include <thread>

// Клас "Дорога"
class Road
{
    public:
    double length;
    double width;
    int numLanes;
    double trafficLevel; // Від 0 (немає трафіку) до 1 (максимальний трафік)

    Road(double length, double width, int numLanes)
        : length(length), width(width), numLanes(numLanes), trafficLevel(0.0) { }

    // Метод для зміни рівня трафіку на дорозі
    void updateTrafficLevel(double newTrafficLevel)
    {
        trafficLevel = newTrafficLevel;
    }
};

// Інтерфейс "Можливість рухатися"
class IDriveable
{
    public:
    virtual void move() = 0;
    virtual void stop() = 0;
};

// Клас "Транспортний засіб"
class Vehicle : public IDriveable
{
public:
    double speed;
double size;
std::string type;

Vehicle(double speed, double size, const std::string& type)
        : speed(speed), size(size), type(type) { }

void move() override
{
    std::cout << "The " << type << " is moving at a speed of " << speed << " km/h." << std::endl;
}

void stop() override
{
    std::cout << "The " << type << " has stopped." << std::endl;
}
};

// Клас для імітації руху транспорту на дорозі
class TrafficSimulation
{
    public:
    TrafficSimulation() { }

    void simulate(Road& road, Vehicle& vehicle, double simulationTime)
    {
        double distance = 0;
        const double timeStep = 1; // Часовий крок для імітації

        while (distance < road.length)
        {
            vehicle.move();
            std::this_thread::sleep_for(std::chrono::seconds(timeStep));
            distance += vehicle.speed * (timeStep / 3600); // Перетворюємо години на секунди

            // Моделюємо зміну рівня трафіку на дорозі
            double newTrafficLevel = distance / road.length;
            road.updateTrafficLevel(newTrafficLevel);
        }

        vehicle.stop();
    }
};

int main()
{
    Road cityRoad(10.0, 3.0, 2); // Приклад дороги у місті
Vehicle car(60.0, 2.0, "car"); // Приклад автомобіля

TrafficSimulation simulation;
simulation.simulate(cityRoad, car, 10.0); // Моделюємо рух автомобіля на дорозі

return 0;
}
