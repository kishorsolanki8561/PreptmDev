import { Injectable } from '@angular/core';
import * as CryptoJS from 'crypto-js';
import { environment } from 'src/environments/environment';
@Injectable({
  providedIn: 'root'
})
export class EncryptionServiceService {
  constructor() { }

  encrypt(data: string): string {
    const key = CryptoJS.enc.Utf8.parse(environment.encryptDecryptKey);  // Example key
    const iv = CryptoJS.enc.Utf8.parse(environment.encryptDecryptKey);   // Example IV
    return CryptoJS.AES.encrypt(data, key, { keySize: 16, iv: iv,mode : CryptoJS.mode.ECB, pandding: CryptoJS.pad.Pkcs7 }).toString();
  }

  decrypt(data: string): string {
    const key = CryptoJS.enc.Utf8.parse(environment.encryptDecryptKey);  // Example key
    const iv = CryptoJS.enc.Utf8.parse(environment.encryptDecryptKey);   // Example IV
    const decrypted = CryptoJS.AES.decrypt(data.toString(), key, { keySize: 16,mode : CryptoJS.mode.ECB, iv: iv, pandding: CryptoJS.pad.Pkcs7 });
    const plaintext = CryptoJS.enc.Utf8.stringify(decrypted);
    return plaintext;
  }   
}
