import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { NewUsersDataService } from './services/new-users-data.service';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{
  title = 'FrontendForChessGame';
  constructor() {
    
  }
  ngOnInit(): void {
    
  }
  
}
