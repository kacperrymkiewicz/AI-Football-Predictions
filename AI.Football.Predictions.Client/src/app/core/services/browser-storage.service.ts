import { Inject, Injectable, InjectionToken } from '@angular/core';

export const BROWSER_STORAGE = new InjectionToken<Storage>('Browser Storage', {
  providedIn: 'root',
  factory: () => localStorage,
});

@Injectable()
export class BrowserStorageService {
  constructor(@Inject(BROWSER_STORAGE) private readonly storage: Storage) {}

  public get<T>(key: string): T | null {
    const item = this.storage.getItem(key);
    return item ? (JSON.parse(item) as T) : null;
  }

  public set<T>(key: string, value: T): void {
    const jsonValue = JSON.stringify(value);
    this.storage.setItem(key, jsonValue);
  }

  public remove(key: string): void {
    this.storage.removeItem(key);
  }

  public clear(): void {
    this.storage.clear();
  }
}
