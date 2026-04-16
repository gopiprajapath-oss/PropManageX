import { Component } from '@angular/core';
import { RouterOutlet } from "@angular/router";
import { SidebarComponent } from "../../../../../shared/sidebar-component/sidebar-component";
import { HeaderComponent } from "../../../../../shared/header-component/header-component";

@Component({
  selector: 'app-user-dashboard-component',
  imports: [RouterOutlet, SidebarComponent, HeaderComponent],
  templateUrl: './user-dashboard-component.html',
  styleUrl: './user-dashboard-component.css',
})
export class UserDashboardComponent {}
