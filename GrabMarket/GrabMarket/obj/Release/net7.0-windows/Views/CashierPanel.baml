   M S B A M L   `   `   `  ���� E  AGrabMarket, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:GrabMarket.ViewsGrabMarket.Views  #    GrabMarket.Views.CashierPanel   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     0localclr-namespace:GrabMarket.Views     -   $��CashierWindow��  � 	Resources     ProductTemp �� BitmapScalingMode �� ElementName  
BasketTemp��%G  &	        &	 x    �� .R��� .+��� $	��100��$	��100��$ HighQuality=����� *
ImageUrl+�� $��	UltraBold"����� *Name+�� $��	UltraBold"����� *Code+]� .+��� $��	UltraBold"�.��1�� $��	UltraBold"����� *Unit+�� $��	UltraBold"����� *Price+�� $��	UltraBold"�.��AZN�� ���� ���� *DataContext.Add+$ CashierWindow��.��Add�� .R��� .+��� $��	UltraBold"����� *Name+�� $��	UltraBold"����� *Count+]� .+��� $��	UltraBold"�$��20(�$��25��
������ 
������ ���� ���� *DataContext.Plus+$ CashierWindow��.��+�� $��	UltraBold"�$��20(�
������ 
������ ���� ���� *DataContext.Minus+$ CashierWindow��.��-�� $��	UltraBold"����� ���� *DataContext.Delete+$ CashierWindow��.��Delete.���  � ColumnDefinitions �� $��* ��� $��2* �.+�� $��0�� � RowDefinitions �� $��* ��� $	��15* ��� $��* ��� $��* �.+��� 
������ 
������ $��0��$��	0,0,203,0q����� *ToMenu+.���� $t��nhttps://c0.klipartz.com/pngpicture/673/795/gratis-png-flecha-azul-ilustracion-flecha-azul-flecha-izquierda.png���� $��15(�
����  �$��	UltraBold"�
���   �$��3������ *
PunchCheck+.��
PunchCheck]� $��2��.+��� $��	UltraBold"�$��13(�.��Total Amount:�� $��	UltraBold"�$��13(����� *TotalAmount+�� $��	UltraBold"�$��13(�.��AZN]� $��1��.+��� 
������ #��[ ���� *Basket+� $��1�� �� $��* ��� $	��10* �.+�]� $��0��.+��� $��53��$	��All������ *
ShowFilter+.��All�� $��60��$��Fruits������ *
ShowFilter+.��Fruits�� $��60��$��
Vegetables������ *
ShowFilter+.��
Vegetables�� $��60��$
��Meat������ *
ShowFilter+.��Meat�� $��60��$��Snacks������ *
ShowFilter+.��Snacks�� $��60��$��Garnish������ *
ShowFilter+.��	Garnish�� $��60��$��Sweets������ *
ShowFilter+.��Sweets�� $��60��$	��Sea������ *
ShowFilter+.��Sea�� $��60��$��Drinks������ *
ShowFilter+.��Drinks�� $��53��$��Other������ *
ShowFilter+.��Other]� $��1��.+��� 
������ #��[  $��Disabled=����� *VisibleProducts+���� .R��  � Orientation$ 
Horizontal=� K� IsItemsHost$
 True��