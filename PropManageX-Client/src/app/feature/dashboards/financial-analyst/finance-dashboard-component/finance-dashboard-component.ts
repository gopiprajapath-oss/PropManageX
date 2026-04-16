import { Component } from '@angular/core';
import { HeaderComponent } from "../../../../shared/header-component/header-component";
import { SidebarComponent } from "../../../../shared/sidebar-component/sidebar-component";
import { RouterOutlet } from "@angular/router";

@Component({
  selector: 'app-finance-dashboard-component',
  imports: [HeaderComponent, SidebarComponent, RouterOutlet],
  templateUrl: './finance-dashboard-component.html',
  styleUrl: './finance-dashboard-component.css',
})
export class FinanceDashboardComponent {}
