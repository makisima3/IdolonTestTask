# Pyton Сервер
Для демонстрации работы сервиса, в папке "Server" в корне проекта, лежит сервер на pyton 3.10.
При необходимости создать виртуальное окружение. 
Перед запуском необходимо установить все зависимости из файла "requirements.txt" командой:

``pip.exe install -r requirements.txt``

Запуск:

``python.exe main.py``

Доступные эндпоинты описаны в коллекции для Postman в файле "maxiserver.postman_collection.json"

Для симуляции задержки работы сервера установлена задержка в фале ``main.py:11``

# Unity приложение

Сервис запускается при старте приложения. 
Имеется возможность оставновки/старта сервиса.

## Кнопки отправки ивентов:
1. level start event
2. level end event
3. level reward event
4. level custom event

## Отображение состояния
- Отображение статуса "Working:On/Off"
- Отображение статуса "sending/Nothing to send"
- Отобрфжение таймера до следующей отправки "Seconds to nesx send:"
- Отображение очереди ивентов на отправку "Events"

## Настройка
ServerUrl устанавливается в игровом объекте "EventService" в компоненте "RestClient"
CooldownBeforeSend устанавливается в игровом объекте "EventService" в компоненте "EventService"

