import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class NewUsersDataService {
 
  constructor(private http: HttpClient) { }

  public dataPleyers(color: string, username: string) {
    const data = `{\"Player\":{\"Name\":\"${username}\"},\"PlayersColor\":{\"Color\":\"${color}\"}}`;
    return this.http.post('http://localhost:5069/api/GameQueue/', data);

  }
}
