import { Component, OnInit } from '@angular/core';
import { NewUsersDataService } from 'src/app/services/new-users-data.service';

@Component({
  selector: 'app-start-window',
  templateUrl: './start-window.component.html',
  styleUrls: ['./start-window.component.scss']
})
export class StartWindowComponent implements OnInit {
  color: string = '';
  username: string = '';
  constructor(private http: NewUsersDataService) { }

  ngOnInit(): void {
    // NewUsersDataService.dataPleyers();
  }

  dataPlayers() {
    console.log(this.username, this.color)
  }
}
