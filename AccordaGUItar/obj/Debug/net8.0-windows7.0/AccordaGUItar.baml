   M S B A M L   `   `   `  ����T  PMahApps.Metro, Version=2.0.0.0, Culture=neutral, PublicKeyToken=51482d6f650b2b3fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  5       AccordaGUItar   Accorda.AccordaGUI   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35:x,http://schemas.microsoft.com/winfx/2006/xaml     5      e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      Kmah;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     5        � Title$  Accordatore per Chitarra��5      $	��500��6&   $	��400��63   (   "MahApps.Metro.Controls.MetroWindow   ShowTitleBar$
 True��5         ShowIconOnTitleBar$
 True��5         TitleCharacterCasing$ Normal=�5       � WindowStartupLocation$ CenterScreen=�5	      .��5      � ���#F2F2F2 � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   +   %MahApps.Metro.Controls.WindowCommands  $��0��6   .@�5      �� -   5        $��Licenza��5      ���#007ACC
�������6W   �� 5      -   5        $��Chiudi��5      ���#007ACC
�������6P   5      �� 5   
   $��Accordatore per Chitarra��6   $��28(�64   $
��Bold"�6B   $��Center=�6T   $��0,20,0,0q�6q   $��1��6�   �� 5   
   -   5         ��Name$ InputDevices��5      $��Center=�6)   $	��300��6F   $��0,20,0,0q�6R   $��2��6�   ��5      �� 5        �� 
TargetType" ��6   .�5!      ��  �� Property � ��	 �� Value
	 �����63   �� 5"       � ��	 �#33333363   �� 5#       � ��	 �#CCCCCC64   �� 5$       � ��$	 1q�68   �� 5%       � ��$	 5q�60   5&      5(      �� 5*   
   $��3��6   
 �� Orientation$
 
Horizontal=�6"   $��Center=�6;   $��0,20,0,0q�6X   .+�5+      �� -   5         ��Name$ FrequenzaLabel��5+      $��Frequenza Rilevata:��60   $��18(�6K   $��0,0,10,0q�6Y   $��Center=�6k   �� 5,      -   5        $ FrequenzaAttuale��5,      $��36(�62   $
��Bold"�6@   $��Center=�6R   5-      �� 5/   
   $��Seleziona Corda:��6   $��18(�6,   $��Center=�6:   $��0,20,0,0q�6W   $��4��6i   �� 51   
   -   5        $ SelezionaCorda��51      $��Center=�6+   $	��300��6H   $��	0,10,0,20q�6T   $��5��6g   ��52      �� 53      " ��6   .�54      ��  � ��	 �#33333363   �� 55       � ��$	 16(�61   �� 56       � ��
	 �����63   57      .@�59      �� $(��"Corda 1 - Mi (alto) [E] - 329.63Hz��6   �� 5:      $!��Corda 2 - Si [B] - 246.94Hz��6   �� 5;      $��Corda 3 - Sol [G] - 196Hz��6   �� 5<      $!��Corda 4 - Re [D] - 146.83Hz��6   �� 5=      $��Corda 5 - La [A] - 110Hz��6   �� 5>      $(��"Corda 6 - Mi (basso) [E] - 82.41Hz��6   5?      5@      5A      