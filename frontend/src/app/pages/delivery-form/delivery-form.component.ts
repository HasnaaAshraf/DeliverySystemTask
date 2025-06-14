import { Component, OnInit } from '@angular/core';
import { SlotService } from '../../services/slot.service';

@Component({
  selector: 'app-delivery-form',
  templateUrl: './delivery-form.component.html',
  styleUrls: ['./delivery-form.component.css']
})

export class DeliveryFormComponent implements OnInit {

  productTypes = [
    { type: 0, name: 'In Stock', selected: false, quantity: 1 },
    { type: 1, name: 'Fresh', selected: false, quantity: 1 },
    { type: 2, name: 'External', selected: false, quantity: 1 },
  ];

  selectedTime: string = '';
  isLoading: boolean = false;
  slots: any[] = [];
  noSlots: boolean = false;

  greenSlots = ['13:00', '14:00', '15:00', '20:00', '21:00', '22:00'];

  constructor(private slotService: SlotService) {}

  ngOnInit(): void {}
  
  checkSlots(): void {

    const selectedProducts = this.productTypes
      .filter(p => p.selected && p.quantity > 0)
      .map(p => ({ productType: p.type, quantity: p.quantity }));

      // Convert selected time to ISO format (UTC-safe for backend)
      const localTime = new Date(this.selectedTime);
      const time = new Date(localTime.getTime() - localTime.getTimezoneOffset() * 60000).toISOString();

    const payload = {
      productDto: selectedProducts,
      time: time,
    };

    this.isLoading = true;
    this.noSlots = false;
    this.slots = [];

    this.slotService.getDeliverySlots(payload).subscribe({
      next: (response) => {
        const result = response.map((slot: any) => ({
          startTime: slot.start,
          endTime: slot.end,
          time: `${slot.start} - ${slot.end}`,
          isGreen: slot.greenSlot
        }));
        
        // Prioritize green slots, then remove duplicates
        const sortedSlots = this.sortSlots(result);
        const uniqueSlots = this.removeDuplicateSlots(sortedSlots);

        this.slots = uniqueSlots;
    
        // Show "No Slots" message if no results returned
        this.noSlots = this.slots.length === 0;
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Error fetching slots:', err);
        this.isLoading = false;
        this.noSlots = true;
      }
    });
  }

  // Sort: green slots first
  sortSlots(slots: any[]): any[] {
    return [
      ...slots.filter(s => s.isGreen),
      ...slots.filter(s => !s.isGreen)
    ];
  }

  // Remove duplicates by start & end time
  removeDuplicateSlots(slots: any[]): any[] {
    const seen = new Set();
    return slots.filter(slot => {
      const key = `${slot.startTime}-${slot.endTime}`;
      if (seen.has(key)) return false;
      seen.add(key);
      return true;
    });
  }

  // return day name from date string
  getDayFromTime(time: string): string {
    return new Date(time).toLocaleDateString('en-US', { weekday: 'long' });
  }
  
} 
