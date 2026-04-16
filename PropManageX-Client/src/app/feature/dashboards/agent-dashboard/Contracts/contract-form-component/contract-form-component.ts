import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { ContractService } from '../contract.service';
 
@Component({
  selector: 'app-contract-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './contract-form-component.html',
  styleUrls: ['./contract-form-component.css']
})
export class ContractFormComponent implements OnInit {
 
  private fb = inject(FormBuilder);
  private service = inject(ContractService);
  private router = inject(Router);
  private route = inject(ActivatedRoute);
 
  isEdit = false;
  contractId!: number;
 
  form = this.fb.group({
    dealID: [0],
    contractType: [''],
    startDate: [''],
    endDate: [''],
    contractValue: [0],
    status: ['']
  });
 
  ngOnInit() {
    const id = this.route.snapshot.params['id'];
 
    if (id) {
      this.isEdit = true;
      this.contractId = id;
 
      this.service.getContractById(id).subscribe(data => {
        this.form.patchValue(data);
      });
    }
  }
 
  submit() {
    if (this.isEdit) {
      this.service.updateContract(this.contractId, this.form.value)
        .subscribe(() => this.router.navigate(['/agent-dashboard/contracts']));
    } else {
      this.service.createContract(this.form.value)
        .subscribe(() => this.router.navigate(['/agent-dashboard/contracts']));
    }
  }
}
 