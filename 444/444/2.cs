#include <iostream>
#include <string>
#include <vector>

// Інтерфейс "Можливість з'єднання"
class IConnectable
{
    public:
    virtual void connect(IConnectable* device) = 0;
    virtual void disconnect(IConnectable* device) = 0;
    virtual void sendData(const std::string& data) = 0;
    virtual void receiveData(const std::string& data) = 0;
};

// Базовий клас "Комп'ютер"
class Computer : public IConnectable
{
public:
    std::string ipAddress;
int power;
std::string osType;
std::vector<IConnectable*> connectedDevices;

Computer(const std::string& ipAddress, int power, const std::string& osType)
        : ipAddress(ipAddress), power(power), osType(osType) { }

void connect(IConnectable* device) override
{
    connectedDevices.push_back(device);
    std::cout << "Connected to " << device->getIpAddress() << std::endl;
}

void disconnect(IConnectable* device) override
{
    // Disconnect logic
    for (size_t i = 0; i < connectedDevices.size(); ++i)
    {
        if (connectedDevices[i] == device)
        {
            connectedDevices.erase(connectedDevices.begin() + i);
            std::cout << "Disconnected from " << device->getIpAddress() << std::endl;
            break;
        }
    }
}

void sendData(const std::string& data) override
{
    // Sending data logic
    for (IConnectable* device : connectedDevices)
    {
        device->receiveData(data);
        std::cout << "Sent data to " << device->getIpAddress() << ": " << data << std::endl;
    }
}

void receiveData(const std::string& data) override
{
    // Receiving data logic
    std::cout << "Received data: " << data << std::endl;
}

const std::string& getIpAddress() const {
        return ipAddress;
    }
};

// Клас "Сервер" (підклас від Computer)
class Server : public Computer
{
public:
    Server(const std::string& ipAddress, int power, const std::string& osType)
        : Computer(ipAddress, power, osType) { }
};

// Клас "Робоча станція" (підклас від Computer)
class Workstation : public Computer
{
public:
    Workstation(const std::string& ipAddress, int power, const std::string& osType)
        : Computer(ipAddress, power, osType) { }
};

// Клас "Маршрутизатор" (підклас від Computer)
class Router : public Computer
{
public:
    Router(const std::string& ipAddress, int power, const std::string& osType)
        : Computer(ipAddress, power, osType) { }
};

// Клас "Мережа"
class Network
{
    public:
    std::vector<Computer*> computers;

    void addComputer(Computer* computer)
    {
        computers.push_back(computer);
    }
};

int main()
{
    Server server("192.168.1.1", 1000, "Linux Server");
Workstation workstation1("192.168.1.2", 500, "Windows 10");
Workstation workstation2("192.168.1.3", 600, "MacOS");
Router router("192.168.1.254", 800, "Router");

Network network;
network.addComputer(&server);
network.addComputer(&workstation1);
network.addComputer(&workstation2);
network.addComputer(&router);

server.connect(&workstation1);
server.connect(&workstation2);
router.connect(&server);

server.sendData("Hello from the server!");

return 0;
}