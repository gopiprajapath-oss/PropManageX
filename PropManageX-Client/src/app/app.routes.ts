import { Routes } from '@angular/router';
import { LandingComponent } from './feature/landing/landing-component';
import { LoginComponent } from './feature/auth/login-component/login-component';
import { RegisterComponent } from './feature/auth/register-component/register-component';
import { AgentDashboardComponent } from './feature/dashboards/agent-dashboard/Dashboard/agent-dashboard-component/agent-dashboard-component'
import { PropertyListComponent } from './feature/dashboards/agent-dashboard/Property-management/property-list-component/property-list-component';
import { PropertyFormComponent } from './feature/dashboards/agent-dashboard/Property-management/property-form-component/property-form-component';
import { LeadListComponent } from './feature/dashboards/agent-dashboard/Leads-management/lead-list-component/lead-list-component';
import { LeadFormComponent } from './feature/dashboards/agent-dashboard/Leads-management/lead-form-component/lead-form-component';
import { DealListComponent } from './feature/dashboards/agent-dashboard/Deals-management/deal-list-component/deal-list-component';
import { UnitListComponent } from './feature/dashboards/agent-dashboard/Property-management/unit-list-component/unit-list-component';
import { ContractFormComponent } from './feature/dashboards/agent-dashboard/Contracts/contract-form-component/contract-form-component';
import { ContractListComponent } from './feature/dashboards/agent-dashboard/Contracts/contract-list-component/contract-list-component';
import { DealFormComponent } from './feature/dashboards/agent-dashboard/Deals-management/deal-form-component/deal-form-component';
import { SiteVisitFormComponent } from './feature/dashboards/agent-dashboard/Site-Visit/site-visit-form-component/site-visit-form-component';
import { SiteVisitListComponent } from './feature/dashboards/agent-dashboard/Site-Visit/site-visit-list-component/site-visit-list-component';
import { UnitFormComponent } from './feature/dashboards/agent-dashboard/Property-management/unit-form-component/unit-form-component';
import { ManagerDashboardComponent } from './feature/dashboards/manager-dashboard/manager-dashboard-component/manager-dashboard-component';
import { ManagerOverviewComponent } from './feature/dashboards/manager-dashboard/Overview/manager-overview-component/manager-overview-component';
import { MaintenanceListComponent } from './feature/dashboards/manager-dashboard/Maintenance-management/maintenance-list-component/maintenance-list-component';
import { MaintenanceFormComponent as ManagerForm } from './feature/dashboards/manager-dashboard/Maintenance-management/maintenance-form-component/maintenance-form-component';
import { VendorListComponent } from './feature/dashboards/manager-dashboard/Vendor-management/vendor-list-component/vendor-list-component';
import { VendorFormComponent } from './feature/dashboards/manager-dashboard/Vendor-management/vendor-form-component/vendor-form-component';
import { VendorEditFormComponent } from './feature/dashboards/manager-dashboard/Vendor-management/vendor-edit-form-component/vendor-edit-form-component';
import { RenewalListComponent as ManagerRenewList } from './feature/dashboards/manager-dashboard/renewal-management/renewal-list-component/renewal-list-component';
import { RenewalFormComponent as ManagerRenewFrom } from './feature/dashboards/manager-dashboard/renewal-management/renewal-form-component/renewal-form-component';
import { AdminDashboardComponent } from './feature/dashboards/admin-dashboard/Dashboard/admin-dashboard-component/admin-dashboard-component';
//import { LeadListComponent as AdminLeadListComponent } from './feature/dashboards/admin-dashboard/Lead-management/lead-list-component/lead-list-component';
import { PropertyFormComponent as AdminPropAddFrom } from './feature/dashboards/admin-dashboard/Property-management/property-form-component/property-form-component';
import { UnitListComponent as AdminUnitListComponent} from './feature/dashboards/admin-dashboard/Property-management/unit-list-component/unit-list-component';
import { UnitFormComponent as AdminUnitFormComponent } from './feature/dashboards/admin-dashboard/Property-management/unit-form-component/unit-form-component';
//import { LeadFormComponent as AdminLeadFormComponent } from './feature/dashboards/admin-dashboard/Lead-management/lead-form-component/lead-form-component';
//import { DealListComponent as AdminDealListComponent } from './feature/dashboards/admin-dashboard/Deal-management/deal-list-component/deal-list-component';
//import { ContractListComponent as AdminContractListComponent } from './feature/dashboards/admin-dashboard/Contract-management/contract-list-component/contract-list-component';
import { PropertyListComponent as adminPropList} from './feature/dashboards/admin-dashboard/Property-management/property-list-component/property-list-component';
import { AmenityListComponent } from './feature/dashboards/admin-dashboard/Property-management/amenity-list-component/amenity-list-component';
import { AmenityFormComponent } from './feature/dashboards/admin-dashboard/Property-management/amenity-form-component/amenity-form-component';
import { UserListComponent } from './feature/dashboards/admin-dashboard/admin-user-component/user-list/user-list-component';
import { AddUserComponent } from './feature/dashboards/admin-dashboard/admin-user-component/admin-userForm-component/add-user-component';
import { AdminRevenueComponent } from './feature/dashboards/admin-dashboard/admin-revenue-component/admin-revenue-component';
import { InvoiceListComponent } from './feature/dashboards/financial-analyst/invoice-list-component/invoice-list-component';
import { FinanceDashboardComponent } from './feature/dashboards/financial-analyst/finance-dashboard-component/finance-dashboard-component';
import { CollectionListComponent } from './feature/dashboards/financial-analyst/collection-list.component/collection-list.component';
import { UserDashboardComponent } from './feature/dashboards/user-dashboard/dashboard/user-dashboard-component/user-dashboard-component';
import { SearchPropertyComponent } from './feature/dashboards/user-dashboard/properties/search-property-component/search-property-component';
import { PropertyDetailsComponent } from './feature/dashboards/user-dashboard/properties/property-details-component/property-details-component';
import { EnquiryFormComponent } from './feature/dashboards/user-dashboard/properties/enquiry-form-component/enquiry-form-component';
import { DashboardOverviewComponent } from './feature/dashboards/user-dashboard/dashboard-overview.component/dashboard-overview.component';
import { TenantAgreementsComponent } from './feature/dashboards/user-dashboard/agreements/agreement-component/agreement-component';
import { TenantMaintenanceComponent } from './feature/dashboards/user-dashboard/tenant-maintenance.component/tenant-maintenance.component';
export const routes: Routes = [
  { path: '', component: LandingComponent }, // Default home page
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
 
  // ---------------------------------------------------------
  // USER DASHBOARD (For Prospects/Guests exploring properties)
  // ---------------------------------------------------------
  {
    path: 'user-dashboard',
    component: UserDashboardComponent ,
    children: [
      { path: '', redirectTo: 'properties', pathMatch: 'full' },
      { path: 'properties', component: SearchPropertyComponent},
      { path: 'property/:id', component: PropertyDetailsComponent},
      //property/12/enquiry
      { path: 'property/:id/enquiry', component: EnquiryFormComponent },
      { path: 'overview', component: DashboardOverviewComponent },
      { path: 'agreements', component: TenantAgreementsComponent },
      {path: 'maintenance', component: TenantMaintenanceComponent }, // Placeholder
    ]
  },


 
{
    path: 'agent-dashboard',
    component: AgentDashboardComponent,
    children: [
 
     
      { path: 'properties', component: PropertyListComponent },
      { path: 'add-property', component: PropertyFormComponent },
      { path: 'add-property/:id', component: PropertyFormComponent },
 
      { path: 'properties/:id/units', component: UnitListComponent },
      { path: 'edit-unit/:id', component: UnitFormComponent },
     
 
 
      { path: 'leads',component: LeadListComponent },
      { path: 'add-lead',component: LeadFormComponent },
      { path: 'edit-lead/:id', component:LeadFormComponent},
 
      { path: 'deals',component: DealListComponent },
      { path: 'add-deal',component: DealFormComponent },
      {path: 'edit-deal/:id',component: DealFormComponent },
 
      { path: 'site-visit',component: SiteVisitListComponent },
      { path: 'add-site-visit',component: SiteVisitFormComponent },
      { path: 'edit-site-visit/:id',component: SiteVisitFormComponent},
 
      { path: 'contracts', component: ContractListComponent },
      { path: 'add-contract', component: ContractFormComponent },
      { path: 'edit-contract/:id', component: ContractFormComponent }
 
    ]
},

{
    path: 'manager-dashboard',
    component: ManagerDashboardComponent,
    children: [
      { path: '', redirectTo: 'overview', pathMatch: 'full' }, // Default route inside manager dashboard
      { path: 'overview', component: ManagerOverviewComponent },
      
       // Maintenance Domain
       { path: 'maintenance', component: MaintenanceListComponent },
       { path: 'maintenance/edit/:id', component: ManagerForm },
      // Inside your manager-dashboard children routes:
        { path: 'vendors', component: VendorListComponent },
        { path: 'vendors/dispatch/:id', component: VendorFormComponent }, // Handles creation
        { path: 'vendors/edit/:id', component: VendorEditFormComponent }, // Handles updating// ID here is the MaintenanceRequestID
      
      // Contract & Renewal Domain
      { 
      path: 'contracts', 
          loadComponent() { 
          return import('./feature/dashboards/manager-dashboard/Contract-management/contract-list-component/contract-list-component')
          .then(m => m.ContractListComponent); 
          },  
        },
          { path: 'renewals', component: ManagerRenewList },
          { path: 'renewals/edit/:id', component: ManagerRenewFrom }
        ]
      },

      {path: 'admin-dashboard',component: AdminDashboardComponent,
      children: [
      { path: '', redirectTo: 'properties', pathMatch: 'full' }, // Default route
      
      // Map these to the actual Admin view components once you create them
      { path: 'properties', component: adminPropList },
      { path: 'properties/add', component: AdminPropAddFrom },
      { path: 'properties/edit/:id', component: AdminPropAddFrom },
      {path : 'properties/:id/units', component: AdminUnitListComponent },
      //admin-dashboard/properties/add-unit/2
      {path: 'properties/add-unit/:id', component: AdminUnitFormComponent},
      //admin-dashboard/properties/edit-unit/2/4'
      {path: 'properties/edit-unit/:id/:unitId', component: AdminUnitFormComponent},
      //admin-dashboard/properties/2/amenities
      {path: 'properties/:id/amenities', component: AmenityListComponent},
      //admin-dashboard/properties/2/add-amenity
      {path: 'properties/:id/add-amenity', component: AmenityFormComponent},
      {path: 'properties/:id/edit-amenity/:amenityId', component: AmenityFormComponent},

      {path: 'users', component: UserListComponent},
      {path: 'users/new', component: AddUserComponent},
      {path: 'reports', component: AdminRevenueComponent, data: { roles: 'Admin' } }, 
      ] 
      },
      // 1. The parent route catches "/finance-dashboard"

      { path: 'finance-dashboard', 
        component: FinanceDashboardComponent,
        children: [
          { path: '', redirectTo: 'finance-dashboard', pathMatch: 'full' },
          //financial-analyst/invoices
          {path: 'invoices', component: InvoiceListComponent},
          {path: 'collections', component: CollectionListComponent},
          { path: 'reports', component: AdminRevenueComponent, data: { roles: 'Finance Analyst' } },
      
    ]
  },


]
 