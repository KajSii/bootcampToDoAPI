import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { List } from '../models/list';
import { Item } from '../models/item';
import { Observable } from 'rxjs';

const ITEMS_BASE_URL = 'api/items';
const LISTS_BASE_URL = 'api/list';

@Injectable({
    providedIn: 'root'
})
export class ItemApiService {

    token = localStorage.getItem("token");

    constructor(private http: HttpClient) { }


    // Items API
    createItem(listId: string, item: Item): Observable<Item> {
        return this.http.post<Item>(`${ITEMS_BASE_URL}/list/${listId}`, item, { headers: new HttpHeaders().set('authorization', "Bearer " + this.token) });
    }

    getItem(id: string) {
        return this.http.get<Item>(`${ITEMS_BASE_URL}/${id}`, { headers: new HttpHeaders().set('authorization', "Bearer " + this.token) });
    }

    deleteItem(id: string | undefined) {
        return this.http.delete(`${ITEMS_BASE_URL}/${id}`, { headers: new HttpHeaders().set('authorization', "Bearer " + this.token) });
    }

    updateItem(id: string, item: Item) {
        return this.http.put(`${ITEMS_BASE_URL}/${id}`, item, { headers: new HttpHeaders().set('authorization', "Bearer " + this.token) });
    }

    getItems(id: string): Observable<Item[]> {
        return this.http.get<Item[]>(`${ITEMS_BASE_URL}`, { headers: new HttpHeaders().set('authorization', "Bearer " + this.token), params: {listId: id} });
    }

    updateItemStatus(id: string, item: Item) {
        return this.http.put(`${ITEMS_BASE_URL}/status/${id}`, item, { headers: new HttpHeaders().set('authorization', "Bearer " + this.token) });
    }


    // List API
    createList(list: List): Observable<List> {
        return this.http.post<List>(`${LISTS_BASE_URL}`, list, { headers: new HttpHeaders().set('authorization', "Bearer " + this.token) });
    }

    getList(id: string) {
        return this.http.get<List>(`${LISTS_BASE_URL}/${id}`, { headers: new HttpHeaders().set('authorization', "Bearer " + this.token) });
    }

    getLists() {
        return this.http.get<List[]>(`${LISTS_BASE_URL}`, { headers: new HttpHeaders().set('authorization', "Bearer " + this.token) });
    }

    deleteList(id: string) {
        return this.http.delete(`${LISTS_BASE_URL}/${id}`, { headers: new HttpHeaders().set('authorization', "Bearer " + this.token) });
    }

    updateList(id: string, list: List) {
        return this.http.put(`${LISTS_BASE_URL}/${id}`, list, { headers: new HttpHeaders().set('authorization', "Bearer " + this.token) });
    }
}
