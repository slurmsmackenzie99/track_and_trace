   M S B A M L   `   `   `  ����I  ETrackandtrace1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:TrackApplicationTrackApplication  5   	   !    TrackApplication.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:TrackApplication     5   	     � Title$'  !Track Application - Pawel Zmarlak��5   	   $	��500��63   $	��800��6@    � 	Resources 5	         BG �� Color  BC  BS��%�   &	        &	 %     &	 L     �� 5
   
   $ DeepSkyBlue��6%   �� 5   
   $ PaleVioletRed��6%   �� 5   
   $ LightSalmon��6%   5      .��5      � #���  6    � ColumnDefinitions 5   
   �� 5      $��0* �6   �� 5      .+�5   
   �� -   5         ��Name$ add_individual��5   3   #���   6   $��Add a person��6K   $
��Left=�6b   $��103,200,0,0q�6}   $	��Top=�6�   $	��132��6�   $��35��6�   $��1��6�   �� 5   
   -   5        $ add_loc_btn��5   3   #���   6   $��New location��6H   $
��Left=�6_   $��104,294,0,0q�6z   $	��Top=�6�   $	��133��6�   $��35��6�   $��1��6�   �� 5   
   -   5         ��Name$ hading��5   2   #���   6   $��Track Application��6B   $
��Left=�6^   $��
274,73,0,0q�6y   $	��Top=�6�   $��36(�6�   $��1��6�   $��Light"�6�   $��58��6�   $	��280��6�   �� 5   
   -   5        $ rec_visit_btn��5   3   #���   6   $��Add record of a visit��6J   $
��Left=�6j   $��332,200,0,0q�6�   $	��Top=�6�   $	��132��6�   $��35��6�   $��1��6�   �� 5   
   -   5        $ rec_contact_btn��5   3   #���   6   $��Add record of a contact��6L   $
��Left=�6n   $��35��6�   $��332,294,0,0q�6�   $	��Top=�6�   $	��132��6�   $��1��6�   �� 5   
   -   5        $ generate_number_btn��5   3   #���   6   $��Contact Tracker��6P   $
��Left=�6j   $��563,200,0,0q�6�   $	��Top=�6�   $	��132��6�   $��35��6�    J� RenderTransformOrigin$ 0.481,0.527&�6�   $��1��6  �� 5   
   -   5        $ generate_loc_numbers_btn��5   3   #���   6   $��Visit Tracker��6U   $
��Left=�6m   $��563,294,0,0q�6�   $	��Top=�6�   $	��132��6�   $��35��6�   $��1��6�   5      5      