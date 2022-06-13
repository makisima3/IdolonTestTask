import datetime
from pathlib import Path
import json
from typing import List

__db_path = Path("./db.json")


def init():
    if not __db_path.is_file():
        __db_path.parent.mkdir(parents=True, exist_ok=True)
        __db_path.touch()
        delete_all()


def add(events: List[dict]):
    if len(events) <= 0:
        return
    if sum(map(lambda e: not isinstance(e, dict), events)) > 0:
        raise Exception("events type missmatch")
    events = list(map(__add_date, events))
    data = read_all()
    data += events
    __save(data)


def read_all() -> List[dict]:
    with __db_path.open('rt') as file:
        data = json.load(file)
        return data


def delete_all():
    __db_path.write_text('[]', encoding='utf-8')


def __save(data: List[dict]):
    __db_path.write_text(json.dumps(data, indent=2))


def __add_date(event: dict) -> dict:
    event['date'] = str(datetime.datetime.now())
    return event