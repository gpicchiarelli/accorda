   M S B A M L   `   `   `  ����U  QLiveCharts.Wpf, Version=0.9.7.0, Culture=neutral, PublicKeyToken=0bc1f845d1ebb8dfG4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf  5   	    AccordaGUItar   Accorda.AccordaGUI   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Dlvc4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf     5   	     � Title$  Accordatore per Chitarra��5   	   $	��500��6*   $	��400��67   .��5      � ���#F5F5F5$��
0,0,0,-238q�6     � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5	      $
��Auto �6   �� 5
      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   o� .@�5      m� $
��File��6   .@�5      m� -   5        $��Licenza��5      6,   m� 5      -   5        $��Chiudi��5      6+   5      5      �� 5   
   $��Accordatore per Chitarra��6   $��28(�64   $
��Bold"�6B   $��Center=�6T   $��0,20,0,0q�6q   $��0��6�   �� 5   
   -   5        $��InputDevices��5      $��Center=�6'   $	��300��6D   $��0,20,0,0q�6P   $��1��6�   �� 5   
   $��2��6    �� Orientation$ 
Horizontal=�6"   $��Center=�6;   $��0,20,0,0q�6X   .+�5      �� -   5        $��FrequenzaLabel��5      $��Frequenza Rilevata:��6.   $��18(�6I   $��0,0,10,0q�6W   $��Center=�6i   �� 5      -   5        $��FrequenzaAttuale��5      $��36(�60   $
��Bold"�6>   $��Center=�6P   5      �� 5    
   $��Seleziona Corda:��6   $��18(�6,   $��Center=�6:   $��0,20,0,0q�6W   $��3��6i   �� 5"   
   -   5        $��SelezionaCorda��5"      $��Center=�6)   $	��300��6F   $��0,10,0,0q�6R   $��4��6d   .@�5#      �� $(��"Corda 1 - Mi (alto) [E] - 329.63Hz��6   �� 5$      $!��Corda 2 - Si [B] - 246.94Hz��6   �� 5%      $��Corda 3 - Sol [G] - 196Hz��6   �� 5&      $!��Corda 4 - Re [D] - 146.83Hz��6   �� 5'      $��Corda 5 - La [A] - 110Hz��6   �� 5(      $(��"Corda 6 - Mi (basso) [E] - 82.41Hz��6   5)      !   LiveCharts.Wpf.AngularGauge  5*   
   -   5        $��gauge��5*      $��10q�5+      $��5��6'      	FromValue$	 -50Q�5,         ToValue$	 +50Q�5-      $	��200��5.      $	��200��5/         TicksForeground
 �����
�������$
��Bold"�52      $��16(�6-      SectionsInnerRadius$ .6Q�53      55      56      57      