import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { List } from '../models/list';
import { Item } from '../models/item';

const ITEMS_BASE_URL = 'api/Items';
const LISTS_BASE_URL = 'api/List';

@Injectable({
    providedIn: 'root'
})
export class ItemApiService {

    constructor(private http: HttpClient) { }


    // Items API
    createItem(listId: string, item: Item) {
        return this.http.post(`${ITEMS_BASE_URL}/list/${listId}`, item)
    }

    getItem(id: string) {
        return this.http.get<Item>(`${ITEMS_BASE_URL}/${id}`)
    }

    deleteItem(id: string) {
        return this.http.delete(`${ITEMS_BASE_URL}/${id}`)
    }

    updateItem(id: string, item: Item) {
        return this.http.put(`${ITEMS_BASE_URL}/${id}`, item)
    }

    // getItems(id: string) {
    //     return this.http.get<Item[]>(`${ITEMS_BASE_URL}`)
    // }

    getItems() {
        return this.http.get<Item[]>(`${ITEMS_BASE_URL}`)
    }

    updateItemStatus(id: string, item: Item) {
        return this.http.put(`${ITEMS_BASE_URL}/status/${id}`, item)
    }


    // List API
    createList(list: List) {
        return this.http.post(`${LISTS_BASE_URL}`, list)
    }

    getList(id: string) {
        return this.http.get<List>(`${LISTS_BASE_URL}/${id}`)
    }

    getLists() {
        return this.http.get<List[]>(`${LISTS_BASE_URL}`)
    }

    deleteList(id: string) {
        return this.http.delete(`${LISTS_BASE_URL}/${id}`)
    }

    updateList(id: string, list: List) {
        return this.http.put(`${LISTS_BASE_URL}/${id}`, list)
    }
}
