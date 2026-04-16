import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
 
import { SidebarComponent} from '../../../../../shared/sidebar-component/sidebar-component';
import {  HeaderComponent } from '../../../../../shared/header-component/header-component';
import { RouterOutlet } from '@angular/router';
 
@Component({
  selector: 'app-agent-dashboard',
  standalone: true,
  imports: [
    RouterOutlet,
    CommonModule,
    SidebarComponent,
    HeaderComponent
  ],
  templateUrl: './agent-dashboard-component.html',
  styleUrls: ['./agent-dashboard-component.css']
})
export class AgentDashboardComponent {
  
}
 